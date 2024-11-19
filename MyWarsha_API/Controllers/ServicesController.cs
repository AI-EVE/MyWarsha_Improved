using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.ServiceDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ServiceFilters;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICarRepository _carRepository;

        public ServicesController(IServiceRepository serviceRepository, ICarRepository carRepository)
        {
            _serviceRepository = serviceRepository;
            _carRepository = carRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties, [FromQuery] ServiceFilters serviceFilters, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            try
            {
                var services = await _serviceRepository.GetAll(paginationPropreties, serviceFilters.ToExpression(), minPrice, maxPrice);

                return Ok(services);
            }
            catch (Exception e)
            { 
                return BadRequest(e);
            }
        }
    

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var service = await _serviceRepository.Get(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Count([FromQuery] ServiceFilters serviceFilters)
        {
            var count = await _serviceRepository.GetCount(serviceFilters.ToExpression());

            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ServiceCreateDto serviceCreateDto)
        {
            if (serviceCreateDto == null || serviceCreateDto.ServiceFees.Count == 0)
            {
                return BadRequest("Service must have at least one service fee");
            }

            if (!await _carRepository.HasClient(serviceCreateDto.CarId, serviceCreateDto.ClientId))
            {
                return BadRequest("Car does not belong to the client");
            }

            var service = new Service
            {
                CarId = serviceCreateDto.CarId,
                ClientId = serviceCreateDto.ClientId,
                ServiceStatusId = serviceCreateDto.ServiceStatusId,
                Note = serviceCreateDto.Note,
                ProductsToSell = serviceCreateDto.ProductsToSell.Select(p => new ProductToSell
                {
                    PricePerUnit = p.PricePerUnit,
                    Discount = p.Discount,
                    Count = p.Count,
                    Note = p.Note,
                    ProductId = p.ProductId,
                    //ServiceId = p.ServiceId
                    
                }).ToList(),
                ServiceFees = serviceCreateDto.ServiceFees.Select(sf => new ServiceFee
                {
                    Price = sf.Price,
                    Discount = sf.Discount,
                    Notes = sf.Notes,
                    //ServiceId = sf.ServiceId,
                    CategoryId = sf.CategoryId
                }).ToList()
            };

            await _serviceRepository.Add(service);

            bool isSaved = await _serviceRepository.SaveChanges();

            if (!isSaved)
            {
                return BadRequest("Failed to save the service");
            }

            return CreatedAtAction(nameof(Get), new { id = service.Id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceUpdateDto serviceUpdateDto)
        {
            var service = await _serviceRepository.GetById(id);

            if (service == null)
            {
                return NotFound();
            }

            service.ServiceStatusId = serviceUpdateDto.ServiceStatusId ?? service.ServiceStatusId;
            service.Note = serviceUpdateDto.Note ?? service.Note;
            
            bool isDateValid = DateOnly.TryParse(serviceUpdateDto.Date, out DateOnly date);

            if (isDateValid)
            {
                service.Date = date;
            }

            if (serviceUpdateDto.ClientId != null || serviceUpdateDto.CarId != null)
            {
                if (await _carRepository.HasClient(serviceUpdateDto.CarId ?? service.CarId, serviceUpdateDto.ClientId ?? service.ClientId))
                {
                    service.ClientId = serviceUpdateDto.ClientId ?? service.ClientId;
                    service.CarId = serviceUpdateDto.CarId ?? service.CarId;
                }             
            }

            _serviceRepository.Update(service);

            bool isSaved = await _serviceRepository.SaveChanges();

            if (!isSaved)
            {
                return BadRequest("Failed to save the service");
            }

            return NoContent();
        }

    }
}