using MyWarsha_Models.Models;

namespace MyWarsha_Interfaces.ServicesInterfaces.IdentityServicesInterfaces
{
    public interface IJWTTokenGeneratorService
    {
        Task<string> GenerateJwtTokenAsync(AppUser user);
    }
}