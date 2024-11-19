using MyWarsha_Interfaces.ServicesInterfaces;
using MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces;

namespace MyWarsha_Services.AzureServices
{
    public class DeleteImageService : IDeleteImageService
    {
        private readonly IBlobService _blobService;

        public DeleteImageService(IBlobService blobService)
        {
            _blobService = blobService;
        }


        public Task<bool> DeleteImage(string imageUrl)
        {
            return _blobService.DeleteImageAsync(imageUrl);   
        }
    }
}