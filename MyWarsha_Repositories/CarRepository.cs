using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs;
using MyWarsha_DTOs.CarDTOs;
using MyWarsha_DTOs.CarGenerationDtos;
using MyWarsha_DTOs.CarImageDTOs;
using MyWarsha_DTOs.CarMakerDtos;
using MyWarsha_DTOs.CarModelDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.CarFilters;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CarDto?> GetDtoById(int id)
        {
            return await _context.Car.Where(x => x.Id == id).Select(c => new CarDto
            {
                Id = c.Id,
                Color = c.Color,
                PlateNumber = c.PlateNumber,
                ChassisNumber = c.ChassisNumber,
                MotorNumber = c.MotorNumber,
                Notes = c.Notes,
                CarGenerationId = c.CarGenerationId,
                ClientId = c.ClientId,
                CarInfo = new CarInfoDto
                {
                    CarMaker = new CarMakerDto
                    {
                        Id = c.CarGeneration.CarModel.CarMaker.Id,
                        Name = c.CarGeneration.CarModel.CarMaker.Name,
                        Notes = c.CarGeneration.CarModel.CarMaker.Notes,
                        Logo = c.CarGeneration.CarModel.CarMaker.Logo
                    },
                    CarModel = new CarModelDto
                    {
                        Id = c.CarGeneration.CarModel.Id,
                        Name = c.CarGeneration.CarModel.Name,
                        Notes = c.CarGeneration.CarModel.Notes
                    },
                    CarGeneration = new CarGenerationDto
                    {
                        Id = c.CarGeneration.Id,
                        Name = c.CarGeneration.Name,
                        Notes = c.CarGeneration.Notes
                    }
                },
                CarImages = c.CarImages.Select(ci => new CarImageDto
                {
                    Id = ci.Id,
                    ImagePath = ci.ImagePath,
                    IsMain = ci.IsMain,
                    CarId = ci.CarId
                }).ToList()
            }).FirstOrDefaultAsync();
        }



        public async Task<IEnumerable<CarDto>> GetAll(CarFilters filters, PaginationPropreties paginationPropreties)
        {
            var query = _context.Car.AsQueryable();

            if (filters.Color != null)
            {
                query = query.Where(x => x.Color == filters.Color);
            }

            if (filters.PlateNumber != null)
            {
                query = query.Where(x => x.PlateNumber == filters.PlateNumber);
            }

            if (filters.ChassisNumber != null)
            {
                query = query.Where(x => x.ChassisNumber == filters.ChassisNumber);
            }

            if (filters.MotorNumber != null)
            {
                query = query.Where(x => x.MotorNumber == filters.MotorNumber);
            }

            if (filters.ClientId != null)
            {
                query = query.Where(x => x.ClientId == filters.ClientId);
            }

            if (filters.CarGenerationId != null)
            {
                query = query.Where(x => x.CarGenerationId == filters.CarGenerationId);
            }

            if (filters.CarModelId != null)
            {
                query = query.Where(x => x.CarGeneration.CarModelId == filters.CarModelId);
            }

            if (filters.CarMakerId != null)
            {
                query = query.Where(x => x.CarGeneration.CarModel.CarMakerId == filters.CarMakerId);
            }

            query = paginationPropreties.ApplyPagination(query);

            return await query.Select(c => new CarDto
            {
                Id = c.Id,
                Color = c.Color,
                PlateNumber = c.PlateNumber,
                ChassisNumber = c.ChassisNumber,
                MotorNumber = c.MotorNumber,
                Notes = c.Notes,
                CarGenerationId = c.CarGenerationId,
                ClientId = c.ClientId,
                CarInfo = new CarInfoDto
                {
                    CarMaker = new CarMakerDto
                    {
                        Id = c.CarGeneration.CarModel.CarMaker.Id,
                        Name = c.CarGeneration.CarModel.CarMaker.Name,
                        Notes = c.CarGeneration.CarModel.CarMaker.Notes,
                        Logo = c.CarGeneration.CarModel.CarMaker.Logo
                    },
                    CarModel = new CarModelDto
                    {
                        Id = c.CarGeneration.CarModel.Id,
                        Name = c.CarGeneration.CarModel.Name,
                        Notes = c.CarGeneration.CarModel.Notes
                    },
                    CarGeneration = new CarGenerationDto
                    {
                        Id = c.CarGeneration.Id,
                        Name = c.CarGeneration.Name,
                        Notes = c.CarGeneration.Notes
                    }
                },
                CarImages = c.CarImages.Select(ci => new CarImageDto
                {
                    Id = ci.Id,
                    ImagePath = ci.ImagePath,
                    IsMain = ci.IsMain,
                    CarId = ci.CarId
                }).ToList()

            }).ToListAsync();


        }

        public Task<Car?> GetById(int id)
        {
            return _context.Car.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasClient(int carId, int clientId)
        {
            var car = await _context.Car.FirstOrDefaultAsync(x => x.Id == carId);

            if (car == null)
            {
                return false;
            }

            return car.ClientId == clientId;
        }

        public async Task<int> CountFilter(CarFilters filters)
        {
            var query = _context.Car.AsQueryable();

            if (filters.Color != null)
            {
                query = query.Where(x => x.Color == filters.Color);
            }

            if (filters.PlateNumber != null)
            {
                query = query.Where(x => x.PlateNumber == filters.PlateNumber);
            }

            if (filters.ChassisNumber != null)
            {
                query = query.Where(x => x.ChassisNumber == filters.ChassisNumber);
            }

            if (filters.MotorNumber != null)
            {
                query = query.Where(x => x.MotorNumber == filters.MotorNumber);
            }

            if (filters.ClientId != null)
            {
                query = query.Where(x => x.ClientId == filters.ClientId);
            }

            if (filters.CarGenerationId != null)
            {
                query = query.Where(x => x.CarGenerationId == filters.CarGenerationId);
            }

            if (filters.CarModelId != null)
            {
                query = query.Where(x => x.CarGeneration.CarModelId == filters.CarModelId);
            }

            if (filters.CarMakerId != null)
            {
                query = query.Where(x => x.CarGeneration.CarModel.CarMakerId == filters.CarMakerId);
            }

            return await query.CountAsync();
        }


    }
}