using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_DTOs.ProductDTOs;
using MyWarsha_DTOs.ProductImageDTOs;
using MyWarsha_DTOs.ProductToSellDTOs;
using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductToSellFilters;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ProductToSellRepository : Repository<ProductToSell>, IProductToSellRepository
    {
        private readonly AppDbContext _context;

        public ProductToSellRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductToSellDto>> GetAll(ProductToSellFilters filters, PaginationPropreties paginationPropreties)
        {
            var query = _context.ProductToSell.AsQueryable();

            if (filters.PricePerUnit != null)
                query = query.Where(x => x.PricePerUnit == filters.PricePerUnit);

            if (filters.Discount != null)
                query = query.Where(x => x.Discount == filters.Discount);

            if (filters.Count != null)
                query = query.Where(x => x.Count == filters.Count);

            if (filters.IsReturned != null)
                query = query.Where(x => x.IsReturned == filters.IsReturned);

            if (filters.Note != null)
                query = query.Where(x => x.Note == filters.Note);

            if (filters.ProductId != null)
                query = query.Where(x => x.ProductId == filters.ProductId);

            if (filters.ServiceId != null)
                query = query.Where(x => x.ServiceId == filters.ServiceId);

            query = paginationPropreties.ApplyPagination(query);

            return await query.Select(x => new ProductToSellDto
            {
                Id = x.Id,
                PricePerUnit = x.PricePerUnit,
                Discount = x.Discount,
                Count = x.Count,
                IsReturned = x.IsReturned,
                Note = x.Note,
                TotalPriceAfterDiscount = (x.PricePerUnit * x.Count) - x.Discount,
                Product = new ProductDto
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Category = new CategoryDto
                    {
                        Id = x.Product.Category.Id,
                        Name = x.Product.Category.Name
                    },
                    ProductType = new ProductTypeDto
                    {
                        Id = x.Product.ProductType.Id,
                        Name = x.Product.ProductType.Name
                    },
                    ProductBrand = new ProductBrandDto
                    {
                        Id = x.Product.ProductBrand.Id,
                        Name = x.Product.ProductBrand.Name
                    },
                    DateAdded = x.Product.DateAdded,
                    Description = x.Product.Description,
                    ListPrice = x.Product.ListPrice,
                    SalePrice = x.Product.SalePrice,
                    Stock = x.Product.Stock,
                    IsAvailable = x.Product.IsAvailable,

                    ProductImages = x.Product.ProductImages.Select(pi => new ProductImageDto
                    {
                        Id = pi.Id,
                        ImageUrl = pi.ImageUrl,
                        IsMain = pi.IsMain,
                        ProductId = pi.ProductId
                    }).ToList()
                }
            }).AsNoTracking().ToListAsync();
        }

        public async Task<ProductToSellDto?> Get(int id)
        {
            var productToSell = await _context.ProductToSell.Where(x => x.Id == id)
                .Select(x => new ProductToSellDto
                {
                    Id = x.Id,
                    PricePerUnit = x.PricePerUnit,
                    Discount = x.Discount,
                    Count = x.Count,
                    IsReturned = x.IsReturned,
                    Note = x.Note,
                    TotalPriceAfterDiscount = (x.PricePerUnit * x.Count) - x.Discount,
                    Product = new ProductDto
                    {
                        Id = x.Product.Id,
                        Name = x.Product.Name,
                        Category = new CategoryDto
                        {
                            Id = x.Product.Category.Id,
                            Name = x.Product.Category.Name
                        },
                        ProductType = new ProductTypeDto
                        {
                            Id = x.Product.ProductType.Id,
                            Name = x.Product.ProductType.Name
                        },
                        ProductBrand = new ProductBrandDto
                        {
                            Id = x.Product.ProductBrand.Id,
                            Name = x.Product.ProductBrand.Name
                        },
                        DateAdded = x.Product.DateAdded,
                        Description = x.Product.Description,
                        ListPrice = x.Product.ListPrice,
                        SalePrice = x.Product.SalePrice,
                        Stock = x.Product.Stock,
                        IsAvailable = x.Product.IsAvailable,

                        ProductImages = x.Product.ProductImages.Select(pi => new ProductImageDto
                        {
                            Id = pi.Id,
                            ImageUrl = pi.ImageUrl,
                            IsMain = pi.IsMain,
                            ProductId = pi.ProductId
                        }).ToList()
                    }
                })
                .FirstOrDefaultAsync();

            return productToSell;
        }

        public async Task<ProductToSell?> GetById(int id)
        {
            return await _context.ProductToSell.FindAsync(id);
        }

        public async Task<int> GetCount(ProductToSellFilters filters)
        {
            var query = _context.ProductToSell.AsQueryable();

            if (filters.PricePerUnit != null)
                query = query.Where(x => x.PricePerUnit == filters.PricePerUnit);

            if (filters.Discount != null)
                query = query.Where(x => x.Discount == filters.Discount);

            if (filters.Count != null)
                query = query.Where(x => x.Count == filters.Count);

            if (filters.IsReturned != null)
                query = query.Where(x => x.IsReturned == filters.IsReturned);

            if (filters.Note != null)
                query = query.Where(x => x.Note == filters.Note);

            if (filters.ProductId != null)
                query = query.Where(x => x.ProductId == filters.ProductId);

            if (filters.ServiceId != null)
                query = query.Where(x => x.ServiceId == filters.ServiceId);

            return await query.CountAsync();
        }
    }
}