using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyWarsha_Interfaces.ServicesInterfaces;
using MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces;

namespace MyWarsha_Services.AzureServices
{
    public class UploadImageService : IUploadImageService
    {
        private readonly IBlobService _blobService;
        
        public UploadImageService(IBlobService blobService)
        {
            _blobService = blobService;
        }


        public async Task<string?> UploadImage(IFormFile? image)
        {
            string? logoUrl = null;

            if (image != null && image.Length != 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                using var stream = image.OpenReadStream();
                logoUrl = await _blobService.UploadImageAsync(stream, fileName);

                return logoUrl;
            }

            return logoUrl;
        }

        
    }
}