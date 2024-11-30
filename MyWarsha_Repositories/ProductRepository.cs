using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_DTOs.ProductDTOs;
using MyWarsha_DTOs.ProductImageDTOs;
using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductFilters;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductDto?> Get(int id)
        {

            return await _context.Product.Where(p => p.Id == id).Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                },
                ProductType = new ProductTypeDto
                {
                    Id = p.ProductType.Id,
                    Name = p.ProductType.Name
                },
                ProductBrand = new ProductBrandDto
                {
                    Id = p.ProductBrand.Id,
                    Name = p.ProductBrand.Name
                },
                DateAdded = p.DateAdded,
                Description = p.Description,
                ListPrice = p.ListPrice,
                SalePrice = p.SalePrice,
                Stock = p.Stock,
                IsAvailable = p.IsAvailable,

                ProductImages = p.ProductImages.Select(pi => new ProductImageDto
                {
                    Id = pi.Id,
                    ImageUrl = pi.ImageUrl,
                    IsMain = pi.IsMain,
                    ProductId = pi.ProductId
                }).ToList()
            }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<string?> GetProductName(int id)
        {
            return await _context.Product.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductDtoMulti>> GetAll(PaginationPropreties paginationPropreties, ProductFilters filters)
        {
            var query = _context.Product.AsQueryable();

            if (filters.Name != null)
            {
                query = query.Where(p => p.Name.Contains(filters.Name));
            }

            if (filters.CategoryId != null)
            {
                query = query.Where(p => p.CategoryId == filters.CategoryId);
            }

            if (filters.ProductTypeId != null)
            {
                query = query.Where(p => p.ProductTypeId == filters.ProductTypeId);
            }

            if (filters.ProductBrandId != null)
            {
                query = query.Where(p => p.ProductBrandId == filters.ProductBrandId);
            }

            if (filters.IsAvailable != null)
            {
                query = query.Where(p => p.IsAvailable == filters.IsAvailable);
            }

            query = paginationPropreties.ApplyPagination(query);

            return await query.Select(p => new ProductDtoMulti
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Name = p.Name,
                Category = p.Category.Name,
                TypeName = p.ProductType.Name,
                BrandName = p.ProductBrand.Name,
                DateAdded = p.DateAdded,
                Description = p.Description,
                ListPrice = p.ListPrice,
                SalePrice = p.SalePrice,
                Stock = p.Stock,
                IsAvailable = p.IsAvailable,
                MainProductImage = p.ProductImages.Count > 0 ? p.ProductImages.Where(pi => pi.IsMain).Select(pi => new ProductImageDto
                {
                    Id = pi.Id,
                    ImageUrl = pi.ImageUrl,
                    IsMain = pi.IsMain,
                    ProductId = pi.ProductId
                }).FirstOrDefault() : null

            }).AsNoTracking().ToListAsync();


        }


        public Task<Product?> GetById(int id)
        {
            return _context.Product.FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<int> Count(ProductFilters filters)
        {
            var query = _context.Product.AsQueryable();

            if (filters.Name != null)
            {
                query = query.Where(p => p.Name.Contains(filters.Name));
            }

            if (filters.CategoryId != null)
            {
                query = query.Where(p => p.CategoryId == filters.CategoryId);
            }

            if (filters.ProductTypeId != null)
            {
                query = query.Where(p => p.ProductTypeId == filters.ProductTypeId);
            }

            if (filters.ProductBrandId != null)
            {
                query = query.Where(p => p.ProductBrandId == filters.ProductBrandId);
            }

            if (filters.IsAvailable != null)
            {
                query = query.Where(p => p.IsAvailable == filters.IsAvailable);
            }

            return await query.CountAsync();
        }
    }
}