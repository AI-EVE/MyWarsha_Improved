using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.ServiceFeeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ServiceFreeFilters;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesFeeController : ControllerBase
    {
        private readonly IServiceFeeRepository _serviceFeeRepository;

        public ServicesFeeController(IServiceFeeRepository serviceFeeRepository)
        {
            _serviceFeeRepository = serviceFeeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties, [FromQuery] ServiceFeeFilters serviceFeeFilters)
        {
            var servicesFee = await _serviceFeeRepository.GetAll(serviceFeeFilters.GetExpression(), paginationPropreties);
            return Ok(servicesFee);
        }       

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceFee = await _serviceFeeRepository.Get(x => x.Id == id);

            if (serviceFee == null)
            {
                return NotFound();
            }
            return Ok(serviceFee);
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCount([FromQuery] ServiceFeeFilters serviceFeeFilters)
        {
            var count = await _serviceFeeRepository.GetCount(serviceFeeFilters.GetExpression());
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ServiceFeeCreateDto serviceFeeDto)
        {
            var serviceFee = new ServiceFee
            {
                Price = serviceFeeDto.Price,
                Discount = serviceFeeDto.Discount,
                Notes = serviceFeeDto.Notes,
                ServiceId = serviceFeeDto.ServiceId,
                CategoryId = serviceFeeDto.CategoryId
            };

            await _serviceFeeRepository.Add(serviceFee);
            await _serviceFeeRepository.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = serviceFee.Id }, null);
        }

        [HttpPost("bulk")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] List<ServiceFeeCreateDto> servicesFeeDto)
        {
            var servicesFee = servicesFeeDto.Select(x => new ServiceFee
            {
                Price = x.Price,
                Discount = x.Discount,
                Notes = x.Notes,
                ServiceId = x.ServiceId,
                CategoryId = x.CategoryId
            }).ToList();

            await _serviceFeeRepository.AddRange(servicesFee);
            await _serviceFeeRepository.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = servicesFee.First().Id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceFeeUpdateDto serviceFeeDto)
        {
            var serviceFee = await _serviceFeeRepository.GetById(id);
            if (serviceFee == null)
            {
                return NotFound();
            }

            serviceFee.Price = serviceFeeDto.Price ?? serviceFee.Price;
            serviceFee.Discount = serviceFeeDto.Discount ?? serviceFee.Discount;
            serviceFee.Notes = serviceFeeDto.Notes ?? serviceFee.Notes;
            serviceFee.IsReturned = serviceFeeDto.IsReturned ?? serviceFee.IsReturned;
            serviceFee.CategoryId = serviceFeeDto.CategoryId ?? serviceFee.CategoryId;

            _serviceFeeRepository.Update(serviceFee);
            await _serviceFeeRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceFee = await _serviceFeeRepository.GetById(id);
            if (serviceFee == null)
            {
                return NotFound();
            }

            _serviceFeeRepository.Delete(serviceFee);
            await _serviceFeeRepository.SaveChanges();
            return NoContent();
        }
    }
}