using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs;
using MyWarsha_DTOs.CarDTOs;
using MyWarsha_DTOs.CarGenerationDtos;
using MyWarsha_DTOs.CarImageDTOs;
using MyWarsha_DTOs.CarMakerDtos;
using MyWarsha_DTOs.CarModelDTOs;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_DTOs.ClientDTOs;
using MyWarsha_DTOs.ProductBrandDTOs;
using MyWarsha_DTOs.ProductDTOs;
using MyWarsha_DTOs.ProductImageDTOs;
using MyWarsha_DTOs.ProductToSellDTOs;
using MyWarsha_DTOs.ProductTypeDTOs;
using MyWarsha_DTOs.ServiceDTOs;
using MyWarsha_DTOs.ServiceFeeDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ServiceFilters;
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

        public async Task<IEnumerable<ServiceDto>> GetAll(PaginationPropreties paginationPropreties, ServiceFilters filters, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Service.AsQueryable();

            if (filters.ServiceStatusId != null)
            {
                query = query.Where(x => x.ServiceStatusId == filters.ServiceStatusId);
            }

            if (filters.ClientId != null)
            {
                query = query.Where(x => x.ClientId == filters.ClientId);
            }

            if (filters.CarId != null)
            {
                query = query.Where(x => x.CarId == filters.CarId);
            }

            if (filters.DateFrom != null)
            {
                var dateFrom = DateOnly.Parse(filters.DateFrom);
                query = query.Where(x => x.Date >= dateFrom);
            }

            if (filters.DateTo != null)
            {
                var dateTo = DateOnly.Parse(filters.DateTo);
                query = query.Where(x => x.Date <= dateTo);
            }

            if (minPrice != null)
            {
                query = query.Where(x => x.ServiceFees.Sum(sf => sf.Price - sf.Discount) + x.ProductsToSell.Sum(ps => (ps.PricePerUnit - ps.Discount) * ps.Count) >= minPrice);
            }

            if (maxPrice != null)
            {
                query = query.Where(x => x.ServiceFees.Sum(sf => sf.Price - sf.Discount) + x.ProductsToSell.Sum(ps => (ps.PricePerUnit - ps.Discount) * ps.Count) <= maxPrice);
            }


            query = paginationPropreties.ApplyPagination(query);
            return await query.Select(x => new ServiceDto
            {
                Id = x.Id,
                Date = x.Date.ToString("yyyy-MM-dd"),
                TotalPriceAfterDiscount = x.ServiceFees.Sum(sf => sf.Price - sf.Discount) + x.ProductsToSell.Sum(ps => (ps.PricePerUnit - ps.Discount) * ps.Count),
                Client = new ClientDtoForService
                {
                    Id = x.Client.Id,
                    Name = x.Client.Name,
                    Email = x.Client.Email
                },
                Car = new CarDto
                {
                    Id = x.Car.Id,
                    Color = x.Car.Color,
                    PlateNumber = x.Car.PlateNumber,
                    ChassisNumber = x.Car.ChassisNumber,
                    MotorNumber = x.Car.MotorNumber,
                    Notes = x.Car.Notes,
                    ClientId = x.Car.ClientId,
                    CarGenerationId = x.Car.CarGenerationId,
                    CarInfo = new CarInfoDto
                    {
                        CarMaker = new CarMakerDto
                        {
                            Id = x.Car.CarGeneration.CarModel.CarMaker.Id,
                            Name = x.Car.CarGeneration.CarModel.CarMaker.Name,
                            Notes = x.Car.CarGeneration.CarModel.CarMaker.Notes,
                            Logo = x.Car.CarGeneration.CarModel.CarMaker.Logo
                        },
                        CarModel = new CarModelDto
                        {
                            Id = x.Car.CarGeneration.CarModel.Id,
                            Name = x.Car.CarGeneration.CarModel.Name,
                            Notes = x.Car.CarGeneration.CarModel.Notes
                        },
                        CarGeneration = new CarGenerationDto
                        {
                            Id = x.Car.CarGeneration.Id,
                            Name = x.Car.CarGeneration.Name,
                            Notes = x.Car.CarGeneration.Notes
                        }
                    },
                    CarImages = x.Car.CarImages.Select(ci => new CarImageDto
                    {
                        Id = ci.Id,
                        ImagePath = ci.ImagePath,
                        IsMain = ci.IsMain,
                        CarId = ci.CarId
                    }).ToList()
                },
                Status = x.Status,
                Note = x.Note,
                ProductsToSell = x.ProductsToSell.Select(pts => new ProductToSellDto
                {
                    Id = pts.Id,
                    PricePerUnit = pts.PricePerUnit,
                    Discount = pts.Discount,
                    Count = pts.Count,
                    IsReturned = pts.IsReturned,
                    Note = pts.Note,
                    TotalPriceAfterDiscount = (pts.PricePerUnit - pts.Discount) * pts.Count,
                    Product = new ProductDto
                    {
                        Id = pts.Product.Id,
                        Name = pts.Product.Name,
                        Category = new CategoryDto
                        {
                            Id = pts.Product.Category.Id,
                            Name = pts.Product.Category.Name
                        },
                        ProductType = new ProductTypeDto
                        {
                            Id = pts.Product.ProductType.Id,
                            Name = pts.Product.ProductType.Name
                        },
                        ProductBrand = new ProductBrandDto
                        {
                            Id = pts.Product.ProductBrand.Id,
                            Name = pts.Product.ProductBrand.Name
                        },
                        DateAdded = pts.Product.DateAdded,
                        Description = pts.Product.Description,
                        ListPrice = pts.Product.ListPrice,
                        SalePrice = pts.Product.SalePrice,
                        Stock = pts.Product.Stock,
                        IsAvailable = pts.Product.IsAvailable,

                        ProductImages = pts.Product.ProductImages.Select(pi => new ProductImageDto
                        {
                            Id = pi.Id,
                            ImageUrl = pi.ImageUrl,
                            IsMain = pi.IsMain,
                            ProductId = pi.ProductId
                        }).ToList()
                    }
                }).ToList(),
                ServiceFees = x.ServiceFees.Select(sf => new ServiceFeeDto
                {
                    Id = sf.Id,
                    Price = sf.Price,
                    Discount = sf.Discount,
                    IsReturned = sf.IsReturned,
                    Notes = sf.Notes,
                    CategoryId = sf.CategoryId,
                    ServiceId = sf.ServiceId,
                    TotalPriceAfterDiscount = sf.Price - sf.Discount
                }).ToList()
            }).AsNoTracking().ToListAsync();
        }

        public async Task<ServiceDto?> Get(int id)
        {
            return await _context.Service.Where(x => x.Id == id).Select(x => new ServiceDto
            {
                Id = x.Id,
                Date = x.Date.ToString("yyyy-MM-dd"),
                TotalPriceAfterDiscount = x.ServiceFees.Sum(sf => sf.Price - sf.Discount) + x.ProductsToSell.Sum(ps => (ps.PricePerUnit * ps.Count) - ps.Discount),

                Client = new ClientDtoForService
                {
                    Id = x.Client.Id,
                    Name = x.Client.Name,
                    Email = x.Client.Email
                },
                Car = new CarDto
                {
                    Id = x.Car.Id,
                    Color = x.Car.Color,
                    PlateNumber = x.Car.PlateNumber,
                    ChassisNumber = x.Car.ChassisNumber,
                    MotorNumber = x.Car.MotorNumber,
                    Notes = x.Car.Notes,
                    ClientId = x.Car.ClientId,
                    CarGenerationId = x.Car.CarGenerationId,
                    CarInfo = new CarInfoDto
                    {
                        CarMaker = new CarMakerDto
                        {
                            Id = x.Car.CarGeneration.CarModel.CarMaker.Id,
                            Name = x.Car.CarGeneration.CarModel.CarMaker.Name,
                            Notes = x.Car.CarGeneration.CarModel.CarMaker.Notes,
                            Logo = x.Car.CarGeneration.CarModel.CarMaker.Logo
                        },
                        CarModel = new CarModelDto
                        {
                            Id = x.Car.CarGeneration.CarModel.Id,
                            Name = x.Car.CarGeneration.CarModel.Name,
                            Notes = x.Car.CarGeneration.CarModel.Notes
                        },
                        CarGeneration = new CarGenerationDto
                        {
                            Id = x.Car.CarGeneration.Id,
                            Name = x.Car.CarGeneration.Name,
                            Notes = x.Car.CarGeneration.Notes
                        }
                    },
                    CarImages = x.Car.CarImages.Select(ci => new CarImageDto
                    {
                        Id = ci.Id,
                        ImagePath = ci.ImagePath,
                        IsMain = ci.IsMain,
                        CarId = ci.CarId
                    }).ToList()
                },
                Status = x.Status,
                Note = x.Note,
                ProductsToSell = x.ProductsToSell.Select(pts => new ProductToSellDto
                {
                    Id = pts.Id,
                    PricePerUnit = pts.PricePerUnit,
                    Discount = pts.Discount,
                    Count = pts.Count,
                    IsReturned = pts.IsReturned,
                    Note = pts.Note,
                    TotalPriceAfterDiscount = (pts.PricePerUnit * pts.Count) - pts.Discount,
                    Product = new ProductDto
                    {
                        Id = pts.Product.Id,
                        Name = pts.Product.Name,
                        Category = new CategoryDto
                        {
                            Id = pts.Product.Category.Id,
                            Name = pts.Product.Category.Name
                        },
                        ProductType = new ProductTypeDto
                        {
                            Id = pts.Product.ProductType.Id,
                            Name = pts.Product.ProductType.Name
                        },
                        ProductBrand = new ProductBrandDto
                        {
                            Id = pts.Product.ProductBrand.Id,
                            Name = pts.Product.ProductBrand.Name
                        },
                        DateAdded = pts.Product.DateAdded,
                        Description = pts.Product.Description,
                        ListPrice = pts.Product.ListPrice,
                        SalePrice = pts.Product.SalePrice,
                        Stock = pts.Product.Stock,
                        IsAvailable = pts.Product.IsAvailable,

                        ProductImages = pts.Product.ProductImages.Select(pi => new ProductImageDto
                        {
                            Id = pi.Id,
                            ImageUrl = pi.ImageUrl,
                            IsMain = pi.IsMain,
                            ProductId = pi.ProductId
                        }).ToList()
                    }
                }).ToList(),
                ServiceFees = x.ServiceFees.Select(sf => new ServiceFeeDto
                {
                    Id = sf.Id,
                    Price = sf.Price,
                    Discount = sf.Discount,
                    IsReturned = sf.IsReturned,
                    Notes = sf.Notes,
                    CategoryId = sf.CategoryId,
                    ServiceId = sf.ServiceId,
                    TotalPriceAfterDiscount = sf.Price - sf.Discount
                }).ToList()
            }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Service?> GetById(int id)
        {
            return await _context.Service
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount(ServiceFilters filters, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Service.AsQueryable();

            if (filters.ServiceStatusId != null)
            {
                query = query.Where(x => x.ServiceStatusId == filters.ServiceStatusId);
            }

            if (filters.ClientId != null)
            {
                query = query.Where(x => x.ClientId == filters.ClientId);
            }

            if (filters.CarId != null)
            {
                query = query.Where(x => x.CarId == filters.CarId);
            }

            if (filters.DateFrom != null)
            {
                var dateFrom = DateOnly.Parse(filters.DateFrom);
                query = query.Where(x => x.Date >= dateFrom);
            }

            if (filters.DateTo != null)
            {
                var dateTo = DateOnly.Parse(filters.DateTo);
                query = query.Where(x => x.Date <= dateTo);
            }

            if (minPrice != null)
            {
                query = query.Where(x => x.ServiceFees.Sum(sf => sf.Price - sf.Discount) + x.ProductsToSell.Sum(ps => (ps.PricePerUnit * ps.Count) - ps.Discount) >= minPrice);
            }

            if (maxPrice != null)
            {
                query = query.Where(x => x.ServiceFees.Sum(sf => sf.Price - sf.Discount) + x.ProductsToSell.Sum(ps => (ps.PricePerUnit * ps.Count) - ps.Discount) <= maxPrice);
            }

            return await query.CountAsync();
        }


    }
}