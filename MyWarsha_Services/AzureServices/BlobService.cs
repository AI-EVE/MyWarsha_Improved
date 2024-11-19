using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using MyWarsha_Interfaces.ServicesInterfaces;

public class BlobService : IBlobService
{
    private readonly BlobContainerClient _blobContainerClient;

    public BlobService(IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("AzureBlobStorage:ConnectionString").Value;
        var containerName = configuration.GetSection("AzureBlobStorage:ContainerName").Value;
        _blobContainerClient = new BlobContainerClient(connectionString, containerName);
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        
        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = GetContentType(fileName)
        };

        await blobClient.UploadAsync(imageStream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
        return blobClient.Uri.ToString();
    }

    public async Task<bool> DeleteImageAsync(string imageUrl)
    {
        var fileName = GetFileNameFromUrl(imageUrl);
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        bool isDeleted = await blobClient.DeleteIfExistsAsync();
        return isDeleted;
    }

    public string GetFileNameFromUrl(string url)
    {
        return Path.GetFileName(new Uri(url).LocalPath);
    }

    public string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }
}