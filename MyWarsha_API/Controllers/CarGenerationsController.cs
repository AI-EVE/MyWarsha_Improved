using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.CarGenerationDtos;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarGenerationsController : ControllerBase
    {
        private readonly ICarGenerationRepository _carGenerationRepository;

        public CarGenerationsController(ICarGenerationRepository carGenerationRepository)
        {
            _carGenerationRepository = carGenerationRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties)
        {
            IEnumerable<CarGeneration> carGenerations = await _carGenerationRepository.GetAll(paginationPropreties);
            return Ok(carGenerations);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            CarGeneration? carGeneration = await _carGenerationRepository.GetById(id);
            if (carGeneration == null) return NotFound();

            return Ok(carGeneration);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CarGenerationCreateDto carGenerationCreateDto)
        {
            CarGeneration carGeneration = new()
            {
                Name = carGenerationCreateDto.Name,
                Notes = carGenerationCreateDto.Notes,
                CarModelId = carGenerationCreateDto.CarModelId
            };

            await _carGenerationRepository.Add(carGeneration);
            await _carGenerationRepository.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = carGeneration.Id }, carGeneration);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] CarGenerationUpdateDto carGenerationUpdateDto)
        {
            CarGeneration? carGeneration = await _carGenerationRepository.GetById(id);
            if (carGeneration == null) return NotFound();

            carGeneration.Name = carGenerationUpdateDto.Name ?? carGeneration.Name;
            carGeneration.Notes = carGenerationUpdateDto.Notes ?? carGeneration.Notes;

            _carGenerationRepository.Update(carGeneration);
            await _carGenerationRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            CarGeneration? carGeneration = await _carGenerationRepository.GetById(id);
            if (carGeneration == null) return NotFound();

            _carGenerationRepository.Delete(carGeneration);
            await _carGenerationRepository.SaveChanges();

            return NoContent();
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public IActionResult Count()
        {
            int count = _carGenerationRepository.Count();
            return Ok(count);
        }
    }
}