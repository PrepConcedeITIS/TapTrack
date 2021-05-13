using System.Linq;
using System.Threading.Tasks;
using image4ioDotNetSDK;
using image4ioDotNetSDK.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private const string BaseImgLink = "https://cdn.image4.io/mvc2img/f_auto";
        private readonly IConfiguration _configuration;
        private readonly Image4ioAPI _image4IoApi;

        public ImageUploadService(IConfiguration configuration)
        {
            _configuration = configuration;
            var credentials = _configuration.GetSection("Image4IO")
                .GetChildren()
                .ToDictionary(x => x.Key, y => y.Value);
            _image4IoApi = new Image4ioAPI(credentials["Key"], credentials["Secret"]);
        }

        public async Task<string> UploadProjectLogoImageAsync(IFormFile file, string userId, string projectIdVisible)
        {
            var path = $"/projects/{userId}/{projectIdVisible}";
            var fileName = $"logo_{projectIdVisible}_{file.Name}___{file.FileName.GetHashCode()}";

            return await UploadImage(file, path, fileName);
        }

        public async Task<string> UploadUserProfileImage(IFormFile file, string userId)
        {
            var path = $"/path/{userId}";
            var fileName = $"profile_image_{userId}_{file.Name}___{file.FileName.GetHashCode()}";

            return await UploadImage(file, path, fileName);
        }

        private async Task<string> UploadImage(IFormFile file, string path, string fileName)
        {
            await using var stream = file.OpenReadStream();
            var uploadRequestModel = new UploadImageRequest()
            {
                Path = path,
                UseFilename = false,
                Overwrite = false
            };
            uploadRequestModel.Files.Add(new UploadImageRequest.File()
            {
                Data = stream,
                FileName = fileName
            });
            var response = await _image4IoApi.UploadImageAsync(uploadRequestModel);
            if (response.Success)
            {
                var imgName = response.UploadedFiles.FirstOrDefault()?.Name;
                var link = $"{BaseImgLink}{imgName}";
                return link;
            }

            return "";
        }
    }
}