using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ServiceDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceDto>> GetAll(PaginationPropreties paginationPropreties, Expression<Func<Service, bool>> predicate, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Service
                .Include(s => s.Client)
                .Include(s => s.Car)
                    .ThenInclude(c => c.CarInfo)
                        .ThenInclude(ci => ci.CarMaker)
                .Include(s => s.Car)
                    .ThenInclude(c => c.CarInfo)
                        .ThenInclude(ci => ci.CarModel)
                .Include(s => s.Car)
                    .ThenInclude(c => c.CarInfo)
                        .ThenInclude(ci => ci.CarGeneration)
                .Include(c => c.Car)
                    .ThenInclude(ci => ci.CarImages)
                .Include(s => s.Status)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.Category)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.ProductType)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.ProductBrand)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.ProductImages)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.CarInfoProduct)
                .Include(s => s.ServiceFees)
                .Where(predicate)
                .Select(x => ServiceDto.ToServiceDto(x));



            var services = await paginationPropreties.ApplyPagination(query).ToListAsync();

            services.ForEach(s => s.TotalPriceAfterDiscount = s.ServiceFees.Sum(sf => sf.TotalPriceAfterDiscount) + s.ProductsToSell.Sum(ps => ps.TotalPriceAfterDiscount));

            if (minPrice.HasValue)
            {
                services = services.Where(s => s.ServiceFees.Sum(sf => sf.TotalPriceAfterDiscount) + s.ProductsToSell.Sum(ps => ps.TotalPriceAfterDiscount) >= minPrice).ToList();
            }

            if (maxPrice.HasValue)
            {
                services = services.Where(s => s.ServiceFees.Sum(sf => sf.TotalPriceAfterDiscount) + s.ProductsToSell.Sum(ps => ps.TotalPriceAfterDiscount) <= maxPrice).ToList();
            }

            return services;
        }

        public async Task<ServiceDto?> Get(Expression<Func<Service, bool>> predicate)
        {
            var service = await _context.Service
                .Include(s => s.Client)
                .Include(s => s.Car)
                    .ThenInclude(c => c.CarInfo)
                        .ThenInclude(ci => ci.CarMaker)
                .Include(s => s.Car)
                    .ThenInclude(c => c.CarInfo)
                        .ThenInclude(ci => ci.CarModel)
                .Include(s => s.Car)
                    .ThenInclude(c => c.CarInfo)
                        .ThenInclude(ci => ci.CarGeneration)
                .Include(c => c.Car)
                    .ThenInclude(ci => ci.CarImages)
                .Include(s => s.Status)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.Category)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.ProductType)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.ProductBrand)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.ProductImages)
                .Include(s => s.ProductsToSell)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.CarInfoProduct)
                .Include(s => s.ServiceFees)
                .Where(predicate)
                .Select(x => ServiceDto.ToServiceDto(x))
                .FirstOrDefaultAsync();

            if (service != null)
            {
                service.TotalPriceAfterDiscount = service.ServiceFees.Sum(sf => sf.TotalPriceAfterDiscount) + service.ProductsToSell.Sum(ps => ps.TotalPriceAfterDiscount);
            }

            return service;
        }

        public async Task<Service?> GetById(int id)
        {
            return await _context.Service
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount(Expression<Func<Service, bool>> predicate)
        {
            return await _context.Service
                .CountAsync(predicate);
        }
    }
}