namespace MyWarsha_Interfaces.ServicesInterfaces.AzureServicesInterfaces
{
    public interface IDeleteImageService
    {
        Task<bool> DeleteImage(string imageUrl);
    }
}