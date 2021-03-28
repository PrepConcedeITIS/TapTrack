using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController : AuthorizedApiController
    {
        private readonly IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> _getUniquenessQueryHandler;
        private readonly IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto> _getProjectByIdHandler;
        private readonly IAsyncCommandHandler<ProjectEditCommand, ProjectDto> _editProjectHandler;
        private readonly IAsyncCommandHandler<ProjectCreateCommand, ProjectDto> _createProjectHandler;

        public ProjectController(IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> getUniquenessQueryHandler,
            IAsyncCommandHandler<ProjectEditCommand, ProjectDto> editProjectHandler,
            IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto> getProjectByIdHandler,
            IAsyncCommandHandler<ProjectCreateCommand, ProjectDto> createProjectHandler)
        {
            _getUniquenessQueryHandler = getUniquenessQueryHandler;
            _editProjectHandler = editProjectHandler;
            _getProjectByIdHandler = getProjectByIdHandler;
            _createProjectHandler = createProjectHandler;
        }

        [HttpGet("idVisibleAvailability/{idVisible}")]
        public async Task<IActionResult> Get(string idVisible)
        {
            return Ok(await _getUniquenessQueryHandler.Handle(new GetUniquenessOfIdQuery(idVisible)));
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ProjectCreateCommand command)
        {
            var commandWithUser = command with {Claims = User};
            var projectDto = await _createProjectHandler.Handle(commandWithUser);
            return Ok(projectDto);
        }

        [HttpPut("{projectId}/edit")]
        public async Task<IActionResult> UpdateProject([FromForm] ProjectCreateCommand command, Guid projectId)
        {
            var editCommand = new ProjectEditCommand(projectId, command.Name, command.IdVisible, command.Description,
                command.Logo);
            var projectDto = await _editProjectHandler.Handle(editCommand);
            return Ok(projectDto);
        }

        [HttpGet("{projectId}/edit")]
        public async Task<IActionResult> GetProjectForEdit(Guid projectId)
        {
            var result = await _getProjectByIdHandler.Handle(new GetProjectByIdQuery(projectId));
            return Ok(result);
        }
    }
}