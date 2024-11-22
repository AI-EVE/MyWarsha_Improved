using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DTOs.CarDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.CarFilters;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarImageRepository _carImageRepository;
        private readonly IDeleteImageService _deleteImageService;

        public CarsController(ICarRepository carRepository, ICarImageRepository carImageRepository, IDeleteImageService deleteImageService)
        {
            _deleteImageService = deleteImageService;
            _carImageRepository = carImageRepository;
            _carRepository = carRepository;
        }

        // [HttpGet]
        // [ProducesResponseType(200)]
        // public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties)
        // {
        //     var cars = await _carRepository.GetAll(paginationPropreties);
        //     return Ok(cars);
        // }

        // [HttpGet("filter")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] CarFilters carFilters, [FromQuery] PaginationPropreties paginationPropreties)
        {
            var cars = await _carRepository.GetAll(carFilters, paginationPropreties);

            return Ok(cars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _carRepository.GetDtoById(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CarCreateDto carCreateDto)
        {
            var car = new Car
            {
                Color = carCreateDto.Color,
                PlateNumber = carCreateDto.PlateNumber,
                ChassisNumber = carCreateDto.ChassisNumber,
                MotorNumber = carCreateDto.MotorNumber,
                Notes = carCreateDto.Notes,
                ClientId = carCreateDto.ClientId,
                CarGenerationId = carCreateDto.CarGenerationId,
            };

            try
            {
                await _carRepository.Add(car);
                await _carRepository.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = car.Id }, new CarDto
                {
                    Id = car.Id,
                    Color = car.Color,
                    PlateNumber = car.PlateNumber,
                    ChassisNumber = car.ChassisNumber,
                    MotorNumber = car.MotorNumber,
                    Notes = car.Notes,
                    ClientId = car.ClientId,
                    CarGenerationId = car.CarGenerationId,
                });
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "There is already another car with the same plate number." });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Update(int id, [FromBody] CarUpdateDto carUpdateDto)
        {
            var car = await _carRepository.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            car.Color = carUpdateDto.Color ?? car.Color;
            car.PlateNumber = carUpdateDto.PlateNumber ?? car.PlateNumber;
            car.ChassisNumber = carUpdateDto.ChassisNumber ?? car.ChassisNumber;
            car.MotorNumber = carUpdateDto.MotorNumber ?? car.MotorNumber;
            car.Notes = carUpdateDto.Notes ?? car.Notes;
            car.CarGenerationId = carUpdateDto.CarGenerationId ?? car.CarGenerationId;



            try
            {
                _carRepository.Update(car);
                await _carRepository.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "There is already another car with the same plate number." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carRepository.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            try
            {
                _carRepository.Delete(car);
                await _carRepository.SaveChanges();

                var images = await _carImageRepository.GetAllEntities(id);

                foreach (var image in images)
                {
                    await _deleteImageService.DeleteImage(image.ImagePath);
                }

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 547))
            {
                return Conflict(new { message = "This car has a Service You have to Delete the Service First." });
            }
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Count([FromQuery] CarFilters carFilters)
        {
            var count = await _carRepository.CountFilter(carFilters);
            return Ok(count);
        }

    }
}