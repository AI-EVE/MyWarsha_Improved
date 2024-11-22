using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Interfaces.ServicesInterfaces.IdentityServicesInterfaces;
using MyWarsha_Repositories;
using MyWarsha_Services.IdentityServices;

namespace MyWarsha_API.Extensions
{
    public static class ServiceExtesnionsForRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICarMakerRepository, CarMakerRepository>();
            services.AddScoped<ICarModelRepository, CarModelRepository>();
            services.AddScoped<ICarGenerationRepository, CarGenerationRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarImageRepository, CarImageRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductsRestockingBillRepository, ProductsRestockingBillRepository>();
            services.AddScoped<IProductBoughtRepository, ProductBoughtRepository>();
            services.AddScoped<IProductToSellRepository, ProductToSellRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceFeeRepository, ServiceFeeRepository>();
            services.AddScoped<IServiceStatusRepository, ServiceStatusRepository>();
            services.AddScoped<IJWTTokenGeneratorService, JWTTokenGeneratorService>();

            return services;
        }
    }
}