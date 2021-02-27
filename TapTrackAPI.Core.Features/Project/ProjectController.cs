using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController: AuthorizedApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(3);
        }
    }
}