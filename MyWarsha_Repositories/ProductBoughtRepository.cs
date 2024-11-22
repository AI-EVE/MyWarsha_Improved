using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductBoughtDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductBoughtFilters;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ProductBoughtRepository : Repository<ProductBought>, IProductBoughtRepository
    {
        private readonly AppDbContext _context;

        public ProductBoughtRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductBoughtDto?> Get(int id)
        {

            return await _context.ProductBought.Where(x => x.Id == id)
                .Select(x => new ProductBoughtDto
                {
                    Id = x.Id,
                    PricePerUnit = x.PricePerUnit,
                    Discount = x.Discount,
                    Count = x.Count,
                    IsReturned = x.IsReturned,
                    Note = x.Note,
                    TotalPriceAfterDiscount = (x.PricePerUnit * x.Count) - x.Discount,
                    ProductId = x.ProductId,
                    ProductsRestockingBillId = x.ProductsRestockingBillId,
                    productName = x.Product.Name
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductBoughtDto>> GetAll(PaginationPropreties paginationPropreties, ProductBoughtFilters filters)
        {
            var query = _context.ProductBought.AsQueryable();

            query = paginationPropreties.ApplyPagination(query);

            if (filters.Count != null)
            {
                query = query.Where(x => x.Count == filters.Count);
            }

            if (filters.Discount != null)
            {
                query = query.Where(x => x.Discount == filters.Discount);
            }

            if (filters.IsReturned != null)
            {
                query = query.Where(x => x.IsReturned == filters.IsReturned);
            }

            if (filters.MaxPricePerUnit != null)
            {
                query = query.Where(x => x.PricePerUnit <= filters.MaxPricePerUnit);
            }

            if (filters.MinPricePerUnit != null)
            {
                query = query.Where(x => x.PricePerUnit >= filters.MinPricePerUnit);
            }

            if (filters.ProductId != null)
            {
                query = query.Where(x => x.ProductId == filters.ProductId);
            }

            if (filters.ProductsRestockingBillId != null)
            {
                query = query.Where(x => x.ProductsRestockingBillId == filters.ProductsRestockingBillId);
            }

            return await query.Select(x => new ProductBoughtDto
            {
                Id = x.Id,
                PricePerUnit = x.PricePerUnit,
                Discount = x.Discount,
                Count = x.Count,
                IsReturned = x.IsReturned,
                Note = x.Note,
                TotalPriceAfterDiscount = (x.PricePerUnit * x.Count) - x.Discount,
                ProductId = x.ProductId,
                ProductsRestockingBillId = x.ProductsRestockingBillId,
                productName = x.Product.Name
            }).AsNoTracking().ToListAsync();
        }

        public async Task<ProductBought?> GetById(int id)
        {
            return await _context.ProductBought
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount(ProductBoughtFilters filters)
        {
            var query = _context.ProductBought.AsQueryable();

            if (filters.Count != null)
            {
                query = query.Where(x => x.Count == filters.Count);
            }

            if (filters.Discount != null)
            {
                query = query.Where(x => x.Discount == filters.Discount);
            }

            if (filters.IsReturned != null)
            {
                query = query.Where(x => x.IsReturned == filters.IsReturned);
            }

            if (filters.MaxPricePerUnit != null)
            {
                query = query.Where(x => x.PricePerUnit <= filters.MaxPricePerUnit);
            }

            if (filters.MinPricePerUnit != null)
            {
                query = query.Where(x => x.PricePerUnit >= filters.MinPricePerUnit);
            }

            if (filters.ProductId != null)
            {
                query = query.Where(x => x.ProductId == filters.ProductId);
            }

            if (filters.ProductsRestockingBillId != null)
            {
                query = query.Where(x => x.ProductsRestockingBillId == filters.ProductsRestockingBillId);
            }

            return await query.CountAsync();
        }

        public async Task RemoveRangeAsync()
        {
            var allRecords = await _context.ProductBought.ToListAsync();
            _context.ProductBought.RemoveRange(allRecords);
        }
    }
}