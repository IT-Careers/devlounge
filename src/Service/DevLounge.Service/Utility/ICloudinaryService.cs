using Microsoft.AspNetCore.Http;

namespace DevLounge.Service.Utility
{
    public interface ICloudinaryService
    {
        Task<string> UploadFile(IFormFile formFile);
    }
}
