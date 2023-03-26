using Microsoft.AspNetCore.Http;

namespace DevLounge.Service.Utility
{
    public interface ICloudinaryService
    {
        Task<Dictionary<string, object>> UploadFile(IFormFile formFile);
    }
}
