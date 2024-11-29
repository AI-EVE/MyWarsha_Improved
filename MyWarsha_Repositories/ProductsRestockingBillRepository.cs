using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.ProductBoughtDTOs;
using MyWarsha_DTOs.ProductsRestockingBillDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductsRestockingBillFilters;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ProductsRestockingBillRepository : Repository<ProductsRestockingBill>, IProductsRestockingBillRepository
    {
        private readonly AppDbContext _context;

        public ProductsRestockingBillRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductsRestockingBillDto?> Get(int id)
        {
            return await _context.ProductsRestockingBill
                .Where(x => x.Id == id)
                .Select(x => new ProductsRestockingBillDto
                {
                    Id = x.Id,
                    ShopName = x.ShopName,
                    DateOfOrder = x.DateOfOrder.ToString("yyyy-MM-dd"),
                    ProductsBought = x.ProductsBought.Select(pb => new ProductBoughtDto
                    {
                        Id = pb.Id,
                        PricePerUnit = pb.PricePerUnit,
                        Discount = pb.Discount,
                        Count = pb.Count,
                        IsReturned = pb.IsReturned,
                        Note = pb.Note,
                        TotalPriceAfterDiscount = (pb.PricePerUnit - pb.Discount) * pb.Count,
                        ProductId = pb.ProductId,
                        ProductsRestockingBillId = pb.ProductsRestockingBillId,
                        productName = pb.Product.Name
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductsRestockingBillDto>> GetAll(PaginationPropreties paginationPropreties, ProductsRestockingBillFilters filters)
        {
            var query = _context.ProductsRestockingBill.AsQueryable();

            if (filters.ShopName != null)
            {
                query = query.Where(x => x.ShopName.Contains(filters.ShopName));
            }

            if (filters.DateOfOrderFrom != null)
            {
                query = query.Where(x => x.DateOfOrder >= DateOnly.Parse(filters.DateOfOrderFrom));
            }

            if (filters.DateOfOrderTo != null)
            {
                query = query.Where(x => x.DateOfOrder <= DateOnly.Parse(filters.DateOfOrderTo));
            }

            if (filters.MinTotalPrice != null)
            {
                query = query.Where(x => x.ProductsBought.Select(x => (x.PricePerUnit * x.Count) - x.Discount).Sum() >= filters.MinTotalPrice);
            }

            if (filters.MaxTotalPrice != null)
            {
                query = query.Where(x => x.ProductsBought.Select(x => (x.PricePerUnit * x.Count) - x.Discount).Sum() <= filters.MaxTotalPrice);
            }

            query = paginationPropreties.ApplyPagination(query);

            return await query.Select(x => new ProductsRestockingBillDto
            {
                Id = x.Id,
                ShopName = x.ShopName,
                DateOfOrder = x.DateOfOrder.ToString("yyyy-MM-dd"),
                ProductsBought = x.ProductsBought.Select(pb => new ProductBoughtDto
                {
                    Id = pb.Id,
                    PricePerUnit = pb.PricePerUnit,
                    Discount = pb.Discount,
                    Count = pb.Count,
                    IsReturned = pb.IsReturned,
                    Note = pb.Note,
                    TotalPriceAfterDiscount = (pb.PricePerUnit - pb.Discount) * pb.Count,
                    ProductId = pb.ProductId,
                    ProductsRestockingBillId = pb.ProductsRestockingBillId,
                    productName = pb.Product.Name
                }).ToList()
            }).AsNoTracking().ToListAsync();
        }

        public Task<ProductsRestockingBill?> GetById(int id)
        {
            return _context.ProductsRestockingBill
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount(ProductsRestockingBillFilters filters)
        {
            var query = _context.ProductsRestockingBill.AsQueryable();

            if (filters.ShopName != null)
            {
                query = query.Where(x => x.ShopName.Contains(filters.ShopName));
            }

            if (filters.DateOfOrderFrom != null)
            {
                query = query.Where(x => x.DateOfOrder >= DateOnly.Parse(filters.DateOfOrderFrom));
            }

            if (filters.DateOfOrderTo != null)
            {
                query = query.Where(x => x.DateOfOrder <= DateOnly.Parse(filters.DateOfOrderTo));
            }

            if (filters.MinTotalPrice != null)
            {
                query = query.Where(x => x.ProductsBought.Select(x => (x.PricePerUnit * x.Count) - x.Discount).Sum() >= filters.MinTotalPrice);
            }

            if (filters.MaxTotalPrice != null)
            {
                query = query.Where(x => x.ProductsBought.Select(x => (x.PricePerUnit * x.Count) - x.Discount).Sum() <= filters.MaxTotalPrice);
            }

            return await query.CountAsync();
        }

        public async Task RemoveRangeAsync()
        {
            var allRecords = await _context.ProductsRestockingBill.ToListAsync();
            _context.ProductsRestockingBill.RemoveRange(allRecords);
        }
    }
}