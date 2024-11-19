namespace MyWarsha_Interfaces.ServicesInterfaces
{
    public interface IBlobService
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName);
        Task<bool> DeleteImageAsync(string imageUrl);
        string GetContentType(string fileName);
        public string GetFileNameFromUrl(string url);

    }
}