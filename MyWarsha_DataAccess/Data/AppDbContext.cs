using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWarsha_Models.Models;

namespace MyWarsha_DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<CarImage> CarImage { get; set; }
        public DbSet<CarMaker> CarMaker { get; set; }
        public DbSet<CarModel> CarModel { get; set; }
        public DbSet<CarGeneration> CarGeneration { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductBrand> ProductBrand { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductToSell> ProductToSell { get; set; }
        public DbSet<ProductBought> ProductBought { get; set; }
        public DbSet<ProductsRestockingBill> ProductsRestockingBill { get; set; }
        public DbSet<ServiceFee> ServiceFee { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceStatus> ServiceStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceStatus>().HasData(
                new ServiceStatus { Id = 1, Name = "Pending", ColorLight = "#fef9c3", ColorDark = "#854d0e" },
                new ServiceStatus { Id = 2, Name = "InProgress", ColorLight = "#e0f2fe", ColorDark = "#075985" },
                new ServiceStatus { Id = 3, Name = "Done", ColorLight = "#dcfce7", ColorDark = "#166534" },
                new ServiceStatus { Id = 4, Name = "Canceled", ColorLight = "#fecaca", ColorDark = "#991b1b" }
            );

            modelBuilder.Entity<ServiceStatus>(client =>
            {
                client.HasIndex(ss => ss.Name).IsUnique();
            });

            modelBuilder.Entity<Car>(car =>
            {
                car.HasOne(c => c.Client)
                    .WithMany(c => c.Cars)
                    .HasForeignKey(c => c.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                car.HasOne(c => c.CarGeneration)
                    .WithMany(c => c.Cars)
                    .HasForeignKey(c => c.CarGenerationId)
                    .OnDelete(DeleteBehavior.Restrict);

                car.HasIndex(c => c.PlateNumber).IsUnique();
            });

            modelBuilder.Entity<CarMaker>(carMaker =>
            {
                carMaker.HasIndex(cm => cm.Name).IsUnique();
            });

            modelBuilder.Entity<CarGeneration>(carGeneration =>
            {
                carGeneration.HasOne(cg => cg.CarModel)
                    .WithMany(cm => cm.CarGenerations)
                    .HasForeignKey(cg => cg.CarModelId)
                    .OnDelete(DeleteBehavior.Cascade);

                carGeneration.HasIndex(cg => new { cg.Name, cg.CarModelId }).IsUnique();
            });

            modelBuilder.Entity<CarImage>(carImage =>
            {
                carImage.HasOne(ci => ci.Car)
                    .WithMany(c => c.CarImages)
                    .HasForeignKey(ci => ci.CarId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CarModel>(carModel =>
            {
                carModel.HasOne(cm => cm.CarMaker)
                    .WithMany(cm => cm.CarModels)
                    .HasForeignKey(cm => cm.CarMakerId)
                    .OnDelete(DeleteBehavior.Cascade);

                carModel.HasIndex(cm => new { cm.Name, cm.CarMakerId }).IsUnique();
            });

            modelBuilder.Entity<Category>(category =>
            {
                category.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Phone>(phone =>
            {
                phone.HasOne(p => p.Client)
                    .WithMany(c => c.Phones)
                    .HasForeignKey(p => p.ClientId)
                    .OnDelete(DeleteBehavior.Cascade);

                phone.HasIndex(p => new { p.Number, p.ClientId }).IsUnique();
            });

            modelBuilder.Entity<ProductBrand>(productBrand =>
            {
                productBrand.HasIndex(pb => pb.Name).IsUnique();
            });

            modelBuilder.Entity<ProductType>(productType =>
            {
                productType.HasIndex(pt => pt.Name).IsUnique();
            });

            modelBuilder.Entity<Product>(product =>
            {
                product.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                product.HasOne(p => p.ProductBrand)
                    .WithMany(pb => pb.Products)
                    .HasForeignKey(p => p.ProductBrandId)
                    .OnDelete(DeleteBehavior.Restrict);

                product.HasOne(p => p.ProductType)
                    .WithMany(pt => pt.Products)
                    .HasForeignKey(p => p.ProductTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductBought>(productBought =>
            {
                productBought.HasOne(pb => pb.Product)
                    .WithMany(p => p.ProductsBought)
                    .HasForeignKey(pb => pb.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                productBought.HasOne(pb => pb.ProductsRestockingBill)
                    .WithMany(prb => prb.ProductsBought)
                    .HasForeignKey(pb => pb.ProductsRestockingBillId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductImage>(productImage =>
            {
                productImage.HasOne(pi => pi.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(pi => pi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductToSell>(productToSell =>
            {
                productToSell.HasOne(pts => pts.Product)
                    .WithMany(p => p.ProductsToSell)
                    .HasForeignKey(pts => pts.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                productToSell.HasOne(pts => pts.Service)
                    .WithMany(s => s.ProductsToSell)
                    .HasForeignKey(pts => pts.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Service>(service =>
            {
                service.HasOne(s => s.Client)
                    .WithMany(c => c.Services)
                    .HasForeignKey(s => s.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                service.HasOne(s => s.Car)
                    .WithMany(c => c.Services)
                    .HasForeignKey(s => s.CarId)
                    .OnDelete(DeleteBehavior.Restrict);

                service.HasOne(s => s.Status)
                    .WithMany(ss => ss.Services)
                    .HasForeignKey(s => s.ServiceStatusId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ServiceFee>(serviceFee =>
            {
                serviceFee.HasOne(sf => sf.Category)
                    .WithMany(c => c.ServiceFees)
                    .HasForeignKey(sf => sf.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                serviceFee.HasOne(sf => sf.Service)
                    .WithMany(s => s.ServiceFees)
                    .HasForeignKey(sf => sf.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });




        }
    }
}