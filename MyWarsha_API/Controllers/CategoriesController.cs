using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAll();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetDtoById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Search([FromQuery] CategoryFilters filters)
        {


            var category = await _categoryRepository.Get(filters);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
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

            try
            {
                await _categoryRepository.Add(category);
                await _categoryRepository.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = category.Id }, new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "Category already exists" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryUpdateDto.Name ?? category.Name;

            try
            {
                _categoryRepository.Update(category);
                await _categoryRepository.SaveChanges();

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "Category already exists" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            try
            {
                _categoryRepository.Delete(category);
                await _categoryRepository.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                return Conflict(new { message = "Category is in use by a Service fee or a Product" });
            }

        }
    }
}