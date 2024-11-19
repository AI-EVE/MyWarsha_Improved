using Microsoft.AspNetCore.Mvc;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.CarInfoFilters;
using Utils.PageUtils;
using LinqKit;
using MyWarsha_DTOs.CarInfoDTOs;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarInfosController : ControllerBase
    {
        private readonly ICarInfoRepository _carInfoRepository;


        public CarInfosController(ICarInfoRepository carInfoRepository)
        {
            _carInfoRepository = carInfoRepository;
        }

        // [HttpGet]
        // [ProducesResponseType(200)]
        // public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties)
        // {
        //     var carInfos = await _carInfoRepository.GetAll(paginationPropreties);
        //     return Ok(carInfos);
        // }

        // [HttpGet("filter")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] CarInfoFilters carInfoFilters, [FromQuery] PaginationPropreties paginationPropreties)
        {

            var predicate = PredicateBuilder.New<CarInfo>(true);

            if (!string.IsNullOrEmpty(carInfoFilters.CarMakerName))
            {
                predicate = predicate.And(c => c.CarMaker.Name.Contains(carInfoFilters.CarMakerName));
            }

            if (!string.IsNullOrEmpty(carInfoFilters.CarModelName))
            {
                predicate = predicate.And(c => c.CarModel.Name.Contains(carInfoFilters.CarModelName));
            }

            if (!string.IsNullOrEmpty(carInfoFilters.CarGenerationName))
            {
                predicate = predicate.And(c => c.CarGeneration.Name.Contains(carInfoFilters.CarGenerationName));
            }

            var carInfoDTOs = await _carInfoRepository.GetAll(paginationPropreties, predicate);

            return Ok(carInfoDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var carInfoDTO = await _carInfoRepository.Get(x => x.Id == id);

            if (carInfoDTO == null) return NotFound();
        
            return Ok(carInfoDTO);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> Create([FromBody] CarInfoCreateDto carInfoCreateDto)
        {
            var carInfo = new CarInfo
            {
                CarMakerId = carInfoCreateDto.CarMakerId,
                CarModelId = carInfoCreateDto.CarModelId,
                CarGenerationId = carInfoCreateDto.CarGenerationId
            };

            await _carInfoRepository.Add(carInfo);
            await _carInfoRepository.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = carInfo.Id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] CarInfoUpdateDto carInfoUpdateDto)
        {
            var carInfo = await _carInfoRepository.GetById(id);

            if (carInfo == null)
            {
                return NotFound();
            }

            if (carInfoUpdateDto.CarMakerId != null && carInfoUpdateDto.CarMakerId != 0)
            {
                carInfo.CarMakerId = carInfoUpdateDto.CarMakerId.Value;
            }

            if (carInfoUpdateDto.CarModelId != null && carInfoUpdateDto.CarModelId != 0)
            {
                carInfo.CarModelId = carInfoUpdateDto.CarModelId.Value;
            }

            if (carInfoUpdateDto.CarGenerationId != null && carInfoUpdateDto.CarGenerationId != 0)
            {
                carInfo.CarGenerationId = carInfoUpdateDto.CarGenerationId.Value;
            }

            _carInfoRepository.Update(carInfo);
            await _carInfoRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var carInfo = await _carInfoRepository.GetById(id);

            if (carInfo == null)
            {
                return NotFound();
            }

            _carInfoRepository.Delete(carInfo);
            await _carInfoRepository.SaveChanges();

            return Ok();
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            int count = _carInfoRepository.Count();
            return Ok(count);
        }

    }
}