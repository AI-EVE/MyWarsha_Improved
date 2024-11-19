using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductTypeFilters;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypesController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var productTypes = await _productTypeRepository.GetAll();

            return Ok(productTypes);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var productType = await _productTypeRepository.Get(pt => pt.Id == id);

            if (productType == null)
            {
                return NotFound();
            }

            return Ok(productType);
        }

        [AllowAnonymous]
        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Search([FromQuery] ProductTypeFilters filters)
        {
            var predicate = PredicateBuilder.New<ProductType>(true);

            if (filters.Name != null)
            {
                predicate = predicate.And(pt => pt.Name == filters.Name);
            }

            var productType = await _productTypeRepository.Get(predicate);

            if (productType == null)
            {
                return NotFound();
            }

            return Ok(productType);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ProductTypeCreateDto productTypeCreateDto)
        {
            if (productTypeCreateDto == null)
            {
                return BadRequest();
            }

            var productType = new ProductType
            {
                Name = productTypeCreateDto.Name
            };

            await _productTypeRepository.Add(productType);
            await _productTypeRepository.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = productType.Id }, new ProductTypeDto
            {
                Id = productType.Id,
                Name = productType.Name
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductTypeUpdateDto productTypeUpdateDto)
        {
            var productType = await _productTypeRepository.GetById(id);

            if (productType == null)
            {
                return NotFound();
            }

            productType.Name = productTypeUpdateDto.Name ?? productType.Name;
            _productTypeRepository.Update(productType);
            await _productTypeRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var productType = await _productTypeRepository.GetById(id);

            if (productType == null)
            {
                return NotFound();
            }

            _productTypeRepository.Delete(productType);
            await _productTypeRepository.SaveChanges();

            return NoContent();
        }
    }
}