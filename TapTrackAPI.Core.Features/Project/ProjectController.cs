using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Interfaces;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController : AuthorizedApiController
    {
        private readonly IImageUploadService _imageUpload;
        private readonly UserManager<User> _userManager;
        private readonly DbContext _dbContext;

        public ProjectController(IImageUploadService imageUpload, UserManager<User> userManager, DbContext dbContext)
        {
            _imageUpload = imageUpload;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(3);
        }

        [HttpPost,DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromBody] ProjectCreateQuery query)
        {
            var creatorId = _userManager.GetUserId(User);
            //var link = await _imageUpload.UploadProjectLogoImageAsync(query.Logo, creatorId, query.IdVisible);
            //var project = new Entities.Project(query.Name, query.IdVisible, query.Description, link,
            //    Guid.Parse(creatorId));
            //var entityEntry = await _dbContext.Set<Entities.Project>().AddAsync(project);
            //_dbContext.SaveChanges();
            return Ok();
        }
    }
}