using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.CategoryFilters;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
 
            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [AllowAnonymous]
        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Search([FromQuery] CategoryFilters filters)
        {
            var predicate = PredicateBuilder.New<Category>(true);

            if (filters.Name != null)
            {
                predicate = predicate.And(c => c.Name == filters.Name);
            }

            var category = await _categoryRepository.Get(predicate);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null)
            {
                return BadRequest();
            }

            var category = new Category
            {
                Name = categoryCreateDto.Name
            };

            await _categoryRepository.Add(category);
            await _categoryRepository.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            });
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryUpdateDto.Name ?? category.Name;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChanges();

            return NoContent();
        }
    }
}