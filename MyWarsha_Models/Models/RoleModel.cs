using System.ComponentModel.DataAnnotations;

namespace MyWarsha_Models.Models
{
    public class RoleModel
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
    }

}