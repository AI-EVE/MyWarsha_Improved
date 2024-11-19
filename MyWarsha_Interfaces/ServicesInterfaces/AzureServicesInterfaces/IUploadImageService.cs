using Microsoft.AspNetCore.Http;

namespace MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces
{
    public interface IUploadImageService
    {
        Task<string?> UploadImage(IFormFile? image);
    }
}