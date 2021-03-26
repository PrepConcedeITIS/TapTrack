using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.Project.Records;
using TapTrackAPI.Core.Interfaces;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController : AuthorizedApiController
    {
        private readonly IImageUploadService _imageUpload;
        private readonly UserManager<User> _userManager;
        private readonly DbContext _dbContext;
        private readonly IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> _getUniquenessQueryHandler;

        public ProjectController(IImageUploadService imageUpload, UserManager<User> userManager, DbContext dbContext,
            IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> getUniquenessQueryHandler)
        {
            _imageUpload = imageUpload;
            _userManager = userManager;
            _dbContext = dbContext;
            _getUniquenessQueryHandler = getUniquenessQueryHandler;
        }

        [HttpGet("idVisibleAvailability/{idVisible}")]
        public async Task<IActionResult> Get(string idVisible)
        {
            return Ok(await _getUniquenessQueryHandler.Handle(new GetUniquenessOfIdQuery(idVisible)));
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ProjectCreateQuery query)
        {
            var creatorId = _userManager.GetUserIdGuid(User);
            var link = await _imageUpload.UploadProjectLogoImageAsync(query.Logo, creatorId.ToString(),
                query.IdVisible);
            var project = new Entities.Project(query.Name, query.IdVisible, query.Description, link, creatorId);
            var entityEntry = await _dbContext.Set<Entities.Project>().AddAsync(project);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}