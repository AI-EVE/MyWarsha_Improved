using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.CarImageDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageRepository _carImageRepository;
        private readonly IDeleteImageService _deleteImageService;
        private readonly IUploadImageService _uploadImageService;

        public CarImagesController(ICarImageRepository carImageRepository, IDeleteImageService deleteImageService, IUploadImageService uploadImageService)
        {
            _carImageRepository = carImageRepository;
            _deleteImageService = deleteImageService;
            _uploadImageService = uploadImageService;
        }


        [HttpGet("{carId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CarImageDto>>> GetCarImages(int carId)
        {
            var carImages = await _carImageRepository.GetAll(carId);

            if (carImages == null)
            {
                return NotFound();
            }

            return Ok(carImages);
        }


        [HttpGet("MainImage/{carId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CarImageDto>> GetMainImage(int carId)
        {
            var mainImage = await _carImageRepository.GetMainImage(carId);

            if (mainImage == null)
            {
                return NotFound();
            }

            return Ok(mainImage);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CarImageDto>> AddCarImage([FromForm] CarImageCreateDto carImageCreateDto)
        {
            var carImage = new CarImage
            {
                CarId = carImageCreateDto.CarId,
                IsMain = carImageCreateDto.IsMain
            };

            if (carImage.IsMain)
            {
                var mainImage = await _carImageRepository.GetMainImageEntity(carImage.CarId);

                if (mainImage != null)
                {
                    mainImage.IsMain = false;
                    _carImageRepository.Update(mainImage);
                }
            }

            string? imgPath = await _uploadImageService.UploadImage(carImageCreateDto.Image);

            if (imgPath == null)
            {
                return BadRequest();
            }

            carImage.ImagePath = imgPath;

            await _carImageRepository.Add(carImage);
            await _carImageRepository.SaveChanges();

            return CreatedAtAction(nameof(GetCarImages), new { carId = carImage.CarId }, new CarImageDto
            {
                Id = carImage.Id,
                ImagePath = carImage.ImagePath,
                IsMain = carImage.IsMain,
                CarId = carImage.CarId
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCarImage(int id)
        {
            var carImage = await _carImageRepository.GetById(id);

            if (carImage == null)
            {
                return NotFound();
            }

            await _deleteImageService.DeleteImage(carImage.ImagePath);

            _carImageRepository.Delete(carImage);
            await _carImageRepository.SaveChanges();

            return NoContent();
        }

        [HttpPut("MainImage/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        
        public async Task<IActionResult> SetMainImage(int id)
        {
            var carImage = await _carImageRepository.GetById(id);

            if (carImage == null)
            {
                return NotFound();
            }

            var mainImage = await _carImageRepository.GetMainImageEntity(carImage.CarId);

            if (mainImage != null)
            {
                mainImage.IsMain = false;
                _carImageRepository.Update(mainImage);
            }

            carImage.IsMain = true;
            _carImageRepository.Update(carImage);

            await _carImageRepository.SaveChanges();

            return NoContent();
        }
    }
}