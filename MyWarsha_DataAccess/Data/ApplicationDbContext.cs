using Microsoft.EntityFrameworkCore;
using MyWarsha_Models.Models;

namespace MyWarsha_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarInfo> CarInfos { get; set; } 
        public DbSet<CarMaker> CarMakers { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarGeneration> CarGenerations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductToSell> ProductsToSell { get; set; }
        public DbSet<ServiceFee> ServiceFees { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}