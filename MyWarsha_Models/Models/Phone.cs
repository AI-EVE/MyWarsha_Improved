using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWarsha_Models.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; } = null!;
        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;
        public int ClientId { get; set; }
    }
}