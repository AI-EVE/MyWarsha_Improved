using System.Linq.Expressions;
using MyWarsha_DTOs.PhoneDTOs;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.PhoneFilters;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IPhoneRepository : IRepository<Phone>
    {
        Task<IEnumerable<PhoneDto>> GetAll(PhoneFilters filters, PaginationPropreties paginationPropreties);
        Task<PhoneDto?> Get(PhoneFilters filters);
        Task<Phone?> GetById(int id);
        // write deleterange methode

        int Count(PhoneFilters filters);
        Task<PhoneDto?> GetPhoneDtoById(int id);
    }
}