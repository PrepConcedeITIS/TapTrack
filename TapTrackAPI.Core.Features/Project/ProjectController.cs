using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController: ApiBaseController
    {
        private readonly IImageUploadService _imageUpload;

        public ProjectController(IImageUploadService imageUpload)
        {
            _imageUpload = imageUpload;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _imageUpload.UploadImage(null, "", "");
            return Ok(3);
        }
    }
}