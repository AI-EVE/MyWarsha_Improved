using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DTOs.ProductDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductFilters;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] ProductFilters filters, [FromQuery] PaginationPropreties paginationPropreties)
        {
            var products = await _productRepository.GetAll(paginationPropreties, filters);

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null)
            {
                return BadRequest();
            }

            Product product = new Product
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                ListPrice = productCreateDto.ListPrice,
                CategoryId = productCreateDto.CategoryId,
                ProductTypeId = productCreateDto.ProductTypeId,
                ProductBrandId = productCreateDto.ProductBrandId,
                SalePrice = productCreateDto.SalePrice,
                Stock = productCreateDto.Stock,
                IsAvailable = productCreateDto.IsAvailable
            };



            try
            {
                await _productRepository.Add(product);
                await _productRepository.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, new { ProdcutId = product.Id });
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2601 || sqlException.Number == 2627))
            {
                return Conflict(new { message = "Product name already exists" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productUpdateDto.Name ?? product.Name;
            product.CategoryId = productUpdateDto.CategoryId ?? product.CategoryId;
            product.Description = productUpdateDto.Description ?? product.Description;
            product.ListPrice = productUpdateDto.ListPrice ?? product.ListPrice;
            product.SalePrice = productUpdateDto.SalePrice ?? product.SalePrice;
            product.Stock = productUpdateDto.Stock ?? product.Stock;
            product.IsAvailable = productUpdateDto.IsAvailable ?? product.IsAvailable;

            _productRepository.Update(product);
            await _productRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Delete(int id, IProductImageRepository productImageRepository, IDeleteImageService deleteImageService)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            var images = await productImageRepository.GetAllByProductId(id);

            var imagesUrls = images.Select(i => i.ImageUrl).ToList();

            try
            {
                _productRepository.Delete(product);
                await _productRepository.SaveChanges();

                foreach (var imageUrl in imagesUrls)
                {
                    await deleteImageService.DeleteImage(imageUrl);
                }

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                return Conflict(new { message = "This product is used in product bought or a product sold and cant be removed" });
            }
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Count([FromQuery] ProductFilters filters)
        {

            var count = await _productRepository.Count(filters);

            return Ok(count);
        }

    }
}