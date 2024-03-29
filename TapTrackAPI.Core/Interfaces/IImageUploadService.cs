using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TapTrackAPI.Core.Interfaces
{
    public interface IImageUploadService
    {
        public Task<string> UploadUserProfileImage(IFormFile file, string userId);
        public Task<string> UploadProjectLogoImageAsync(IFormFile file, string userId, string projectIdVisible);
    }
}