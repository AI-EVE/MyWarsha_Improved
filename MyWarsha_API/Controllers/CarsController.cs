using LinqKit;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly IDeleteImageService _deleteImageService;

        public CarsController(ICarRepository carRepository, ICarImageRepository carImageRepository, IDeleteImageService deleteImageService, ICarInfoRepository carInfoRepository)
        {
            _deleteImageService = deleteImageService;
            _carImageRepository = carImageRepository;
            _carRepository = carRepository;
            _carInfoRepository = carInfoRepository;
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
        public async Task<IActionResult> GetAll([FromQuery] CarFilters carFilters,[FromQuery] PaginationPropreties paginationPropreties)
        {
            var predicate = PredicateBuilder.New<Car>(true);

            if (!string.IsNullOrEmpty(carFilters.PlateNumber))
            {
                predicate = predicate.And(c => c.PlateNumber.Contains(carFilters.PlateNumber));
            }

            if (!string.IsNullOrEmpty(carFilters.Color))
            {
                predicate = predicate.And(c => c.Color != null && c.Color == carFilters.Color);
            }

            if (!string.IsNullOrEmpty(carFilters.ChassisNumber))
            {
                predicate = predicate.And(c => c.ChassisNumber != null && c.ChassisNumber.StartsWith(carFilters.ChassisNumber));
            }

            if (!string.IsNullOrEmpty(carFilters.MotorNumber))
            {
                predicate = predicate.And(c => c.MotorNumber != null && c.MotorNumber.StartsWith(carFilters.MotorNumber));
            }

            if (carFilters.ClientId != null)
            {
                predicate = predicate.And(c => c.ClientId == carFilters.ClientId);
            }

            if (carFilters.CarInfoId != null)
            {
                predicate = predicate.And(c => c.CarInfoId == carFilters.CarInfoId);
            }

            var cars = await _carRepository.GetAll(predicate, paginationPropreties);

            return Ok(cars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _carRepository.Get(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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
                CarInfoId = carCreateDto.CarInfoId
            };

            await _carRepository.Add(car);
            await _carRepository.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, new {CarId = car.Id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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

            if (carUpdateDto.CarInfoId != null)
            {
                var CheckCarInfo = await _carInfoRepository.GetById(carUpdateDto.CarInfoId.Value);
                if (CheckCarInfo != null)
                {
                    car.CarInfoId = carUpdateDto.CarInfoId.Value;
                }
            }

            _carRepository.Update(car);
            await _carRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carRepository.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            var carImages = await _carImageRepository.GetAllEntities(id);

            foreach (var carImage in carImages)
            {
                await _deleteImageService.DeleteImage(carImage.ImagePath);
            }

            _carRepository.Delete(car);

            await _carRepository.SaveChanges();

            return NoContent();
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Count([FromQuery] CarFilters carFilters)
        {

            var predicate = PredicateBuilder.New<Car>(true);

            if (!string.IsNullOrEmpty(carFilters.PlateNumber))
            {
                predicate = predicate.And(c => c.PlateNumber.Contains(carFilters.PlateNumber));
            }

            if (!string.IsNullOrEmpty(carFilters.Color))
            {
                predicate = predicate.And(c => c.Color != null && c.Color == carFilters.Color);
            }

            if (!string.IsNullOrEmpty(carFilters.ChassisNumber))
            {
                predicate = predicate.And(c => c.ChassisNumber != null && c.ChassisNumber.StartsWith(carFilters.ChassisNumber));
            }

            if (!string.IsNullOrEmpty(carFilters.MotorNumber))
            {
                predicate = predicate.And(c => c.MotorNumber != null && c.MotorNumber.StartsWith(carFilters.MotorNumber));
            }

            if (carFilters.ClientId != null)
            {
                predicate = predicate.And(c => c.ClientId == carFilters.ClientId);
            }

            if (carFilters.CarInfoId != null)
            {
                predicate = predicate.And(c => c.CarInfoId == carFilters.CarInfoId);
            }

            var count = await _carRepository.CountFilter(predicate);
            return Ok(count);
        }

    }
}