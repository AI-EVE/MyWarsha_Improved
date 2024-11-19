using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.CarMakerDtos;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarMakersController : ControllerBase
    {
        private readonly ICarMakerRepository _carMakerRepository;
        private readonly IUploadImageService _uploadImageService;
        private readonly IDeleteImageService _deleteImageService;
        

        public CarMakersController(ICarMakerRepository carMakerRepository, IUploadImageService uploadImageService, IDeleteImageService deleteImageService)
        {
            _carMakerRepository = carMakerRepository;
            _uploadImageService = uploadImageService;
            _deleteImageService = deleteImageService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties)
        {
            IEnumerable<CarMaker> carMakers = await _carMakerRepository.GetAll(paginationPropreties);
            return Ok(carMakers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            CarMaker? carMaker = await _carMakerRepository.GetById(id);
            if (carMaker == null) return NotFound();

            return Ok(carMaker);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromForm] CarMakerCreateDto carMakerCreateDto)
        {

           string? logoUrl = await _uploadImageService.UploadImage(carMakerCreateDto.Logo);
            

            CarMaker carMaker = new()
            {
                Name = carMakerCreateDto.Name,
                Notes = carMakerCreateDto.Notes,
                Logo = logoUrl
            };

            await _carMakerRepository.Add(carMaker);

            await _carMakerRepository.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = carMaker.Id }, carMaker);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromForm] CarMakerUpdateDto carMakerUpdateDto)
        {
            CarMaker? carMaker = await _carMakerRepository.GetById(id);
            if (carMaker == null) return NotFound();

            if (carMakerUpdateDto.Name != null)
            {
                carMaker.Name = carMakerUpdateDto.Name;
            }

            if (carMakerUpdateDto.Notes != null)
            {
                carMaker.Notes = carMakerUpdateDto.Notes;
            }

            string? logoUrl = await _uploadImageService.UploadImage(carMakerUpdateDto.Logo);
           
            if (logoUrl != null)
            {
                if (carMaker.Logo != null)
                {
                    await _deleteImageService.DeleteImage(carMaker.Logo);
                }

                carMaker.Logo = logoUrl;
            }
            _carMakerRepository.Update(carMaker);
            await _carMakerRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            CarMaker? carMaker = await _carMakerRepository.GetById(id);
            if (carMaker == null) return NotFound();

            if (carMaker.Logo != null)
            {
                await _deleteImageService.DeleteImage(carMaker.Logo);
            }

            _carMakerRepository.Delete(carMaker);
            await _carMakerRepository.SaveChanges();

            return NoContent();
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public IActionResult Count()
        {
            int count = _carMakerRepository.Count();
            return Ok(count);
        }

    }
}