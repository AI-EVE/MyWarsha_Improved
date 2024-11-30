using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var productBrands = await _productBrandRepository.GetAll();

            return Ok(productBrands);
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public IActionResult Count()
        {
            var count = _productBrandRepository.Count();

            return Ok(count);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var productBrand = await _productBrandRepository.GetProductBrandDtoById(id);

            if (productBrand == null)
            {
                return NotFound();
            }

            return Ok(productBrand);
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Search([FromQuery] ProductBrandFilters filters)
        {

            var productBrand = await _productBrandRepository.Get(filters);

            if (productBrand == null)
            {
                return NotFound();
            }

            return Ok(productBrand);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] ProductBrandCreateDto productBrandCreateDto)
        {
            var productBrand = new ProductBrand
            {
                Name = productBrandCreateDto.Name
            };

            try
            {
                await _productBrandRepository.Add(productBrand);
                await _productBrandRepository.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = productBrand.Id }, new ProductBrandDto
                {
                    Id = productBrand.Id,
                    Name = productBrand.Name
                });
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "Product Brand already exists" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductBrandUpdateDto productBrandUpdateDto)
        {
            var productBrand = await _productBrandRepository.GetById(id);

            if (productBrand == null)
            {
                return NotFound();
            }

            productBrand.Name = productBrandUpdateDto.Name ?? productBrand.Name;


            try
            {
                _productBrandRepository.Update(productBrand);
                await _productBrandRepository.SaveChanges();

                return Ok(productBrand);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
            {
                return Conflict(new { message = "Product Brand already exists" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Delete(int id)
        {
            var productBrand = await _productBrandRepository.GetById(id);

            if (productBrand == null)
            {
                return NotFound();
            }

            try
            {
                _productBrandRepository.Delete(productBrand);
                await _productBrandRepository.SaveChanges();

                return NoContent();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                return Conflict(new { message = "Product Brand is in use by a product, you should delete the product first then try again." });
            }
        }
    }
}