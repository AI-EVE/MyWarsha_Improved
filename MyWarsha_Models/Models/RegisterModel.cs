using System.ComponentModel.DataAnnotations;

namespace MyWarsha_Models.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Email { get; set; }   = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}