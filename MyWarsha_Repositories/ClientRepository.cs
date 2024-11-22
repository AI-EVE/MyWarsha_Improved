using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DataAccess.Data;
using MyWarsha_DTOs;
using MyWarsha_DTOs.CarDTOs;
using MyWarsha_DTOs.CarGenerationDtos;
using MyWarsha_DTOs.CarImageDTOs;
using MyWarsha_DTOs.CarMakerDtos;
using MyWarsha_DTOs.CarModelDTOs;
using MyWarsha_DTOs.ClientDTOs;
using MyWarsha_DTOs.PhoneDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ClientFilters;
using Utils.PageUtils;

namespace MyWarsha_Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClientDto?> Get(Expression<Func<Client, bool>> filter)
        {
            var query = _context.Client.Where(filter);

            return await query.Select(client => new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Phones = client.Phones.Select(p => new PhoneDto
                {
                    Id = p.Id,
                    Number = p.Number
                }).ToList(),
                Cars = client.Cars.Select(c => new CarDto
                {
                    Id = c.Id,
                    Color = c.Color,
                    PlateNumber = c.PlateNumber,
                    ChassisNumber = c.ChassisNumber,
                    MotorNumber = c.MotorNumber,
                    Notes = c.Notes,
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
                }).ToList()
            }).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ClientDtoMulti>> GetAll(ClientFilters filters, PaginationPropreties paginationPropreties)
        {
            var query = _context.Client.AsQueryable();

            if (filters.Name != null)
            {
                query = query.Where(x => x.Name.Contains(filters.Name));
            }

            if (filters.Email != null)
            {
                query = query.Where(x => x.Email != null && x.Email.Contains(filters.Email));
            }

            if (filters.Phone != null)
            {
                query = query.Where(x => x.Phones.Any(p => p.Number.Contains(filters.Phone)));
            }

            query = paginationPropreties.ApplyPagination(query);

            return await query.Select(client => new ClientDtoMulti
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Phones = client.Phones.Select(p => new PhoneDto
                {
                    Id = p.Id,
                    Number = p.Number
                }).ToList(),
                CarsCount = client.Cars.Count()

            }).AsNoTracking().ToListAsync();
        }

        public async Task<Client?> GetById(int id)
        {
            return await _context.Client.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasCar(int clientId, int carId)
        {
            var client = await _context.Client.FirstOrDefaultAsync(c => c.Id == clientId);


            if (client == null) return false;

            return client.Cars.Any(c => c.Id == carId);
        }

        public async Task<bool> HasPhone(int clientId, int phoneId)
        {
            var client = await _context.Client.FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null) return false;

            return client.Phones.Any(p => p.Id == phoneId);
        }

        public async Task<int> FilterCount(ClientFilters filters)
        {
            var query = _context.Client.AsQueryable();

            if (filters.Name != null)
            {
                query = query.Where(x => x.Name.Contains(filters.Name));
            }

            if (filters.Email != null)
            {
                query = query.Where(x => x.Email != null && x.Email.Contains(filters.Email));
            }

            if (filters.Phone != null)
            {
                query = query.Where(x => x.Phones.Any(p => p.Number.Contains(filters.Phone)));
            }

            return await query.CountAsync();
        }
    }
}