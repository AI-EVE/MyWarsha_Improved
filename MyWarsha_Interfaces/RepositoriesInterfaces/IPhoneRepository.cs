using System.Linq.Expressions;
using MyWarsha_DTOs.PhoneDTOs;
using MyWarsha_Models.Models;
using Utils.PageUtils;

namespace MyWarsha_Interfaces.RepositoriesInterfaces
{
    public interface IPhoneRepository : IRepository<Phone>
    {
        Task<IEnumerable<PhoneDto>> GetAll(Expression<Func<Phone, bool>> predicate, PaginationPropreties paginationPropreties);
        Task<PhoneDto?> Get(Expression<Func<Phone, bool>> predicate);
        Task<Phone?> GetById(int id);
        // write deleterange methode

        int Count(Expression<Func<Phone, bool>> predicate);
    }
}