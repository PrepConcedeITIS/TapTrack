using System;
using System.Linq;
using System.Threading.Tasks;
using image4ioDotNetSDK;
using image4ioDotNetSDK.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TapTrackAPI.Core.Interfaces;

namespace TapTrack.Core.Utilities.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly string _baseImgLink;
        private readonly Image4ioAPI _image4IoApi;

        public ImageUploadService(IConfiguration configuration, IHostEnvironment environment)
        {
            var (image4IoApi, baseImageLink) = GetImageStorageApiSetup(configuration, environment);
            _image4IoApi = image4IoApi;
            _baseImgLink = baseImageLink;
        }

        private (Image4ioAPI, string) GetImageStorageApiSetup(IConfiguration configuration,
            IHostEnvironment environment)
        {
            var image4IoKeyEnvironment = Environment.GetEnvironmentVariable("Image4IO_Key");
            var image4IoSecretEnvironment = Environment.GetEnvironmentVariable("Image4IO_Secret");
            if (!environment.IsDevelopment() && image4IoKeyEnvironment != null && image4IoSecretEnvironment != null)
            {
                return (new Image4ioAPI(image4IoKeyEnvironment, image4IoSecretEnvironment),
                    "https://cdn.image4.io/taptrack/f_auto");
            }

            var credentials = configuration.GetSection("Image4IO")
                .GetChildren()
                .ToDictionary(x => x.Key, y => y.Value);
            var api = new Image4ioAPI(credentials["Key"], credentials["Secret"]);
            return (api, "https://cdn.image4.io/mvc2img/f_auto");
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
                var link = $"{_baseImgLink}{imgName}";
                return link;
            }

            return "";
        }
    }
}