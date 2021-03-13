using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TapTrackAPI.Core.Interfaces
{
    public interface IImageUploadService
    {
        public Task<string> UploadImage(IFormFile file, string userId, string projectId);
    }
}