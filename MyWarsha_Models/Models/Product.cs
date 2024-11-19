using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWarsha_Models.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; } = null!;
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductBrandId")]
        public ProductBrand ProductBrand { get; set; } = null!;
        public int ProductBrandId { get; set; }
        public DateOnly DateAdded { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal ListPrice { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }
        public List<CarInfoProduct> CarInfoProduct { get; set; } = [];
        public List<ProductImage> ProductImages { get; set; } = [];
        public List<ProductToSell> ProductsToSell { get; set; } = [];
        public List<ProductBought> ProductsBought { get; set; } = [];
    }
}