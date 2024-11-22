using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromForm] CarMakerCreateDto carMakerCreateDto)
        {

            string? logoUrl = await _uploadImageService.UploadImage(carMakerCreateDto.Logo);


            CarMaker carMaker = new()
            {
                Name = carMakerCreateDto.Name,
                Notes = carMakerCreateDto.Notes,
                Logo = logoUrl
            };

            try
            {
                await _carMakerRepository.Add(carMaker);

                await _carMakerRepository.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = carMaker.Id }, carMaker);
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                if (logoUrl != null)
                    await _deleteImageService.DeleteImage(logoUrl);

                return Conflict(new { message = "CarMaker already exists" });
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Update(int id, [FromForm] CarMakerUpdateDto carMakerUpdateDto)
        {
            CarMaker? carMaker = await _carMakerRepository.GetById(id);
            if (carMaker == null) return NotFound();

            carMaker.Name = carMakerUpdateDto.Name ?? carMaker.Name;

            carMaker.Notes = carMakerUpdateDto.Notes ?? carMaker.Notes;

            string? logoUrl = await _uploadImageService.UploadImage(carMakerUpdateDto.Logo);
            string? oldUrl = carMaker.Logo;

            carMaker.Logo = logoUrl ?? carMaker.Logo;
            _carMakerRepository.Update(carMaker);


            try
            {
                await _carMakerRepository.SaveChanges();
                if (oldUrl != null && logoUrl != null)
                    await _deleteImageService.DeleteImage(oldUrl);

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                if (logoUrl != null)
                    await _deleteImageService.DeleteImage(logoUrl);

                return Conflict(new { message = "CarMaker already exists" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Delete(int id)
        {
            CarMaker? carMaker = await _carMakerRepository.GetById(id);
            if (carMaker == null) return NotFound();

            try
            {
                _carMakerRepository.Delete(carMaker);
                await _carMakerRepository.SaveChanges();

                if (carMaker.Logo != null)
                    await _deleteImageService.DeleteImage(carMaker.Logo);

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                return Conflict(new { message = "This car maker is being used in a car that has at least one car that has a service." });
            }
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