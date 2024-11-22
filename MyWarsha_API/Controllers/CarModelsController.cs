using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DTOs.CarModelDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarModelsController : ControllerBase
    {
        private readonly ICarModelRepository _carModelRepository;

        public CarModelsController(ICarModelRepository carModelRepository)
        {
            _carModelRepository = carModelRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties)
        {
            IEnumerable<CarModel> carModels = await _carModelRepository.GetAll(paginationPropreties);
            return Ok(carModels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            CarModel? carModel = await _carModelRepository.GetById(id);
            if (carModel == null) return NotFound();

            return Ok(carModel);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CarModelCreateDto carModelCreateDto)
        {
            CarModel carModel = new()
            {
                Name = carModelCreateDto.Name,
                Notes = carModelCreateDto.Notes,
                CarMakerId = carModelCreateDto.CarMakerId
            };

            try
            {
                await _carModelRepository.Add(carModel);
                await _carModelRepository.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = carModel.Id }, carModel);
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "CarModel already exists" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Update(int id, [FromBody] CarModelUpdateDto carModelUpdateDto)
        {
            CarModel? carModel = await _carModelRepository.GetById(id);
            if (carModel == null) return NotFound();

            carModel.Name = carModelUpdateDto.Name ?? carModel.Name;
            carModel.Notes = carModelUpdateDto.Notes ?? carModel.Notes;

            try
            {
                _carModelRepository.Update(carModel);
                await _carModelRepository.SaveChanges();

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "CarModel already exists" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Delete(int id)
        {
            CarModel? carModel = await _carModelRepository.GetById(id);
            if (carModel == null) return NotFound();

            try
            {
                _carModelRepository.Delete(carModel);
                await _carModelRepository.SaveChanges();

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                return Conflict(new { message = "CarModel is in use by a car that does already have a service." });
            }
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public IActionResult Count()
        {
            int count = _carModelRepository.Count();
            return Ok(count);
        }
    }
}