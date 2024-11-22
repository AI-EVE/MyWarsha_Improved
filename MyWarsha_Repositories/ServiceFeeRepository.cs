using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ServiceFeeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ServiceFreeFilters;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ServiceFeeRepository : Repository<ServiceFee>, IServiceFeeRepository
    {
        private readonly AppDbContext _context;

        public ServiceFeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceFeeDto>> GetAll(ServiceFeeFilters filters, PaginationPropreties paginationPropreties)
        {
            var query = _context.ServiceFee.AsQueryable();

            if (filters.Price != null)
            {
                query = query.Where(x => x.Price == filters.Price);
            }

            if (filters.Discount != null)
            {
                query = query.Where(x => x.Discount == filters.Discount);
            }

            if (filters.IsReturned != null)
            {
                query = query.Where(x => x.IsReturned == filters.IsReturned);
            }

            if (filters.Notes != null)
            {
                query = query.Where(x => x.Notes == filters.Notes);
            }

            if (filters.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == filters.CategoryId);
            }

            if (filters.ServiceId != null)
            {
                query = query.Where(x => x.ServiceId == filters.ServiceId);
            }

            query = paginationPropreties.ApplyPagination(query);

            return await query.Select(x => new ServiceFeeDto
            {
                Id = x.Id,
                Price = x.Price,
                Discount = x.Discount,
                IsReturned = x.IsReturned,
                Notes = x.Notes,
                CategoryId = x.CategoryId,
                ServiceId = x.ServiceId,
                TotalPriceAfterDiscount = x.Price - x.Discount
            }).AsNoTracking().ToListAsync();
        }

        public async Task<ServiceFeeDto?> Get(int id)
        {
            var serviceFee = await _context.ServiceFee
                .Where(x => x.Id == id)
                .Select(x => new ServiceFeeDto
                {
                    Id = x.Id,
                    Price = x.Price,
                    Discount = x.Discount,
                    IsReturned = x.IsReturned,
                    Notes = x.Notes,
                    CategoryId = x.CategoryId,
                    ServiceId = x.ServiceId,
                    TotalPriceAfterDiscount = x.Price - x.Discount
                }).AsNoTracking().FirstOrDefaultAsync();

            return serviceFee;
        }

        public async Task<ServiceFee?> GetById(int id)
        {
            return await _context.ServiceFee.FindAsync(id);
        }

        public async Task<int> GetCount(ServiceFeeFilters filters)
        {
            var query = _context.ServiceFee.AsQueryable();

            if (filters.Price != null)
            {
                query = query.Where(x => x.Price == filters.Price);
            }

            if (filters.Discount != null)
            {
                query = query.Where(x => x.Discount == filters.Discount);
            }

            if (filters.IsReturned != null)
            {
                query = query.Where(x => x.IsReturned == filters.IsReturned);
            }

            if (filters.Notes != null)
            {
                query = query.Where(x => x.Notes == filters.Notes);
            }

            if (filters.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == filters.CategoryId);
            }

            if (filters.ServiceId != null)
            {
                query = query.Where(x => x.ServiceId == filters.ServiceId);
            }

            return await query.CountAsync();
        }
    }
}