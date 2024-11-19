using MyWarsha_DataAccess.Data;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_Repositories
{
    public class CarinfoProductRepository : Repository<CarInfoProduct>, ICarinfoProductRepository
    {
        private readonly AppDbContext _context;

        public CarinfoProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        
        
    }
}