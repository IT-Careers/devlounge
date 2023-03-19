using Microsoft.AspNetCore.Http;

namespace DevLounge.Service.Utility
{
    public class CloudinaryService : ICloudinaryService
    {

        public async Task<string> UploadFile(IFormFile formFile)
        {
            //string formFileId = Guid.NewGuid().ToString();

            //if (formFile.Length > 0)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        formFile.CopyTo(ms);
            //        byte[] formBytes = ms.ToArray();

            //        ImageUploadParams imageUploadParams = new ImageUploadParams
            //        {
            //            File = new FileDescription(formFileId, new MemoryStream(formBytes)),
            //            PublicId = formFileId
            //        };

            //        ImageUploadResult uploadResult = await this.cloudinary.UploadAsync(imageUploadParams);
            //        return uploadResult.Url.AbsoluteUri;
            //    }
            //}

            return null;
        }
    }
}
