using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.ProductImageDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IUploadImageService _uploadImageService;
        private readonly IDeleteImageService _deleteImageService;

        public ProductImagesController(IProductImageRepository productImageRepository, IUploadImageService uploadImageService, IDeleteImageService deleteImageService)
        {
            _productImageRepository = productImageRepository;
            _uploadImageService = uploadImageService;
            _deleteImageService = deleteImageService;
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllByProductId(int productId)
        {
            IEnumerable<ProductImageDto> productImages = await _productImageRepository.GetAll(pi => pi.ProductId == productId);

            return Ok(productImages);
        }

        [HttpGet("main/{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMainByProductId(int productId)
        {
            ProductImageDto? productImage = await _productImageRepository.Get(pi => pi.ProductId == productId && pi.IsMain);

            if (productImage == null)
            {
                return NotFound();
            }

            return Ok(productImage);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromForm] ProductImageCreateDto productImageCreateDto)
        {
            ProductImage productImage = new ProductImage
            {
                IsMain = productImageCreateDto.IsMain,
                ProductId = productImageCreateDto.ProductId
            };

            string? Url = await _uploadImageService.UploadImage(productImageCreateDto.Image);

            if (Url == null)
            {
                return BadRequest();
            }

            productImage.ImageUrl = Url;

            if (productImage.IsMain)
            {
                ProductImage? mainProductImage = await _productImageRepository.GetMainByProductId(productImage.ProductId);

                if (mainProductImage != null)
                {
                    mainProductImage.IsMain = false;
                    _productImageRepository.Update(mainProductImage);
                }

            }

            await _productImageRepository.Add(productImage);
            await _productImageRepository.SaveChanges();

            return CreatedAtAction(nameof(GetMainByProductId), new { productId = productImage.ProductId }, new ProductImageDto
            {
                Id = productImage.Id,
                ImageUrl = productImage.ImageUrl,
                IsMain = productImage.IsMain,
                ProductId = productImage.ProductId
            });
        }

        [HttpPost("AddMulty/{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCarImages(int productId, [FromForm] List<IFormFile> productImages)
        {
            var images = new List<ProductImage>();

            foreach (var image in productImages)
            {
                string? imageUrl = await _uploadImageService.UploadImage(image);

                if (imageUrl == null)
                {
                    return BadRequest();
                }

                images.Add(new ProductImage
                {
                    ProductId = productId,
                    ImageUrl = imageUrl,
                    IsMain = false
                });
            }

            await _productImageRepository.AddRange(images);
            await _productImageRepository.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SetMainImage(int id)
        {
            ProductImage? productImage = await _productImageRepository.GetById(id);

            if (productImage == null)
            {
                return NotFound();
            }

            ProductImage? mainProductImage = await _productImageRepository.GetMainByProductId(productImage.ProductId);

            if (mainProductImage != null)
            {
                mainProductImage.IsMain = false;
                _productImageRepository.Update(mainProductImage);
            }

            productImage.IsMain = true;
            _productImageRepository.Update(productImage);
            await _productImageRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            ProductImage? productImage = await _productImageRepository.GetById(id);

            if (productImage == null)
            {
                return NotFound();
            }

            bool isDeleted = await _deleteImageService.DeleteImage(productImage.ImageUrl);

            if (!isDeleted)
            {
                return NotFound();
            }

            _productImageRepository.Delete(productImage);
            await _productImageRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("main/{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMain(int productId)
        {
            ProductImage? productImage = await _productImageRepository.GetMainByProductId(productId);

            if (productImage == null)
            {
                return NotFound();
            }

            bool isDeleted = await _deleteImageService.DeleteImage(productImage.ImageUrl);

            if (!isDeleted)
            {
                return NotFound();
            }

            _productImageRepository.Delete(productImage);
            await _productImageRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("DeleteMany")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteManyCarImages(List<int> ids)
        {
            var images = await _productImageRepository.GetByProductIds(ids);
            var imageUrls = images.Select(i => i.ImageUrl).ToList();

            _productImageRepository.DeleteAll(images);

            await _productImageRepository.SaveChanges();

            foreach (var url in imageUrls)
            {
                await _deleteImageService.DeleteImage(url);
            }

            return NoContent();
        }

    }
}