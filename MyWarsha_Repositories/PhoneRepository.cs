using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.PhoneDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.PhoneFilters;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        private readonly AppDbContext _context;

        public PhoneRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PhoneDto>> GetAll(PhoneFilters filters, PaginationPropreties paginationPropreties)
        {
            var query = _context.Phone.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Number))
            {
                query = query.Where(x => x.Number.Contains(filters.Number));
            }

            if (filters.ClientId != null)
            {
                query = query.Where(x => x.ClientId == filters.ClientId);
            }

            query = paginationPropreties.ApplyPagination(query);

            return await query.Select(x => new PhoneDto
            {
                Id = x.Id,
                Number = x.Number,
                ClientId = x.ClientId
            }).AsNoTracking().ToListAsync();
        }

        public async Task<PhoneDto?> Get(PhoneFilters filters)
        {
            var query = _context.Phone.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Number))
            {
                query = query.Where(x => x.Number.Contains(filters.Number));
            }

            if (filters.ClientId != null)
            {
                query = query.Where(x => x.ClientId == filters.ClientId);
            }

            return await query.Select(x => new PhoneDto
            {
                Id = x.Id,
                Number = x.Number,
                ClientId = x.ClientId
            }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<PhoneDto?> GetPhoneDtoById(int id)
        {
            return await _context.Phone.Where(x => x.Id == id).Select(x => new PhoneDto
            {
                Id = x.Id,
                Number = x.Number,
                ClientId = x.ClientId
            }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Phone?> GetById(int id)
        {
            return await _context.Phone.FirstOrDefaultAsync(x => x.Id == id);
        }

        public int Count(PhoneFilters filters)
        {
            var query = _context.Phone.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Number))
            {
                query = query.Where(x => x.Number.Contains(filters.Number));
            }

            if (filters.ClientId != null)
            {
                query = query.Where(x => x.ClientId == filters.ClientId);
            }

            return query.Count();
        }
    }
}