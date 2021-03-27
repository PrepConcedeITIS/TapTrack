using System;
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

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController : AuthorizedApiController
    {
        private readonly IImageUploadService _imageUpload;
        private readonly UserManager<User> _userManager;
        private readonly DbContext _dbContext;
        private readonly IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> _getUniquenessQueryHandler;
        private readonly IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto> _getProjectByIdHandler;
        private readonly IAsyncCommandHandler<ProjectEditCommand, ProjectDto> _editProjectHandler;

        public ProjectController(IImageUploadService imageUpload, UserManager<User> userManager, DbContext dbContext,
            IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> getUniquenessQueryHandler,
            IAsyncCommandHandler<ProjectEditCommand, ProjectDto> editProjectHandler,
            IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto> getProjectByIdHandler)
        {
            _imageUpload = imageUpload;
            _userManager = userManager;
            _dbContext = dbContext;
            _getUniquenessQueryHandler = getUniquenessQueryHandler;
            _editProjectHandler = editProjectHandler;
            _getProjectByIdHandler = getProjectByIdHandler;
        }

        [HttpGet("idVisibleAvailability/{idVisible}")]
        public async Task<IActionResult> Get(string idVisible)
        {
            return Ok(await _getUniquenessQueryHandler.Handle(new GetUniquenessOfIdQuery(idVisible)));
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ProjectCreateCommand command)
        {
            var creatorId = _userManager.GetUserIdGuid(User);
            var link = await _imageUpload.UploadProjectLogoImageAsync(command.Logo, creatorId.ToString(),
                command.IdVisible);
            var project = new Entities.Project(command.Name, command.IdVisible, command.Description, link, creatorId);
            var entityEntry = await _dbContext.Set<Entities.Project>().AddAsync(project);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{projectId}/edit")]
        public async Task<IActionResult> UpdateProject([FromForm] ProjectCreateCommand command, Guid projectId)
        {
            var editCommand = new ProjectEditCommand(projectId, command.Name, command.IdVisible, command.Description,
                command.Logo);
            var project = await _editProjectHandler.Handle(editCommand);
            return Ok(project);
        }

        [HttpGet("{projectId}/edit")]
        public async Task<IActionResult> GetProjectForEdit(Guid projectId)
        {
            var result = await _getProjectByIdHandler.Handle(new GetProjectByIdQuery(projectId));
            return Ok(result);
        }
    }
}