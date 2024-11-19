using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductBrandFilters;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBrandsController : ControllerBase
    {
        private readonly IProductBrandRepository _productBrandRepository;

        public ProductBrandsController(IProductBrandRepository productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var productBrands = await _productBrandRepository.GetAll();

            return Ok(productBrands);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var productBrand = await _productBrandRepository.Get(pb => pb.Id == id);

            if (productBrand == null)
            {
                return NotFound();
            }

            return Ok(productBrand);
        }

        [AllowAnonymous]
        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Search([FromQuery] ProductBrandFilters filters)
        {
            var predicate = PredicateBuilder.New<ProductBrand>(true);

            if (filters.Name != null)
            {
                predicate = predicate.And(pb => pb.Name == filters.Name);
            }

            var productBrand = await _productBrandRepository.Get(predicate);

            if (productBrand == null)
            {
                return NotFound();
            }

            return Ok(productBrand);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ProductBrandCreateDto productBrandCreateDto)
        {
            if (productBrandCreateDto == null)
            {
                return BadRequest();
            }

            var productBrand = new ProductBrand
            {
                Name = productBrandCreateDto.Name
            };

            await _productBrandRepository.Add(productBrand);
            await _productBrandRepository.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = productBrand.Id }, new ProductBrandDto
            {
                Id = productBrand.Id,
                Name = productBrand.Name
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductBrandUpdateDto productBrandUpdateDto)
        {
            var productBrand = await _productBrandRepository.GetById(id);

            if (productBrand == null)
            {
                return NotFound();
            }

            productBrand.Name = productBrandUpdateDto.Name ?? productBrand.Name;

            _productBrandRepository.Update(productBrand);
            await _productBrandRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var productBrand = await _productBrandRepository.GetById(id);

            if (productBrand == null)
            {
                return NotFound();
            }

            _productBrandRepository.Delete(productBrand);
            await _productBrandRepository.SaveChanges();

            return NoContent();
        }
    }
}