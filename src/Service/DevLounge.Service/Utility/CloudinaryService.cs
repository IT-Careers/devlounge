using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DevLounge.Service.Utility
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration configuration;

        private const string CloudinaryUrl = "https://api.cloudinary.com/v1_1/{0}/auto/upload";

        public CloudinaryService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private string GetUnixTimestamp()
        {
            return ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        private string GetApiKey()
        {
            return this.configuration["Cloudinary:ApiKey"];
        }

        private string GetApiSecret()
        {
            return this.configuration["Cloudinary:ApiSecret"];
        }

        private string EncodeWithSha256(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                foreach (byte b in result) stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }

        private string GetSignature(string timestamp, string publicId)
        {
            return this.EncodeWithSha256($"public_id={publicId}&timestamp={timestamp}{this.GetApiSecret()}");
        }

        private byte[] ReadFileBytes(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    return ms.ToArray();
                }
            }

            return new byte[0];
        }

        public async Task<Dictionary<string, object>> UploadFile(IFormFile formFile)
        {
            var currentTimestamp = this.GetUnixTimestamp();
            var apiKey = this.GetApiKey();
            var publicId = formFile.FileName;
            var signature = this.GetSignature(currentTimestamp, publicId);

            String file = Convert.ToBase64String(this.ReadFileBytes(formFile));
            var json = JsonConvert.SerializeObject(new
            {
                file = HttpUtility.HtmlEncode($"data:{formFile.ContentType};base64,") + file,
                timestamp = currentTimestamp,
                public_id = publicId,
                api_key = apiKey,
                signature = signature
            });

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, String.Format(CloudinaryUrl, this.configuration["Cloudinary:CloudName"]))
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var httpClient = new HttpClient();
            var httpResponse = await httpClient.SendAsync(httpRequest);

            if(httpResponse.IsSuccessStatusCode)
            {
                var responseJson = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);
            }

            return null;
        }
    }
}
