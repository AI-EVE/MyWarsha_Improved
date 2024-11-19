using System.ComponentModel.DataAnnotations;

namespace MyWarsha_DTOs.CategoryDTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}