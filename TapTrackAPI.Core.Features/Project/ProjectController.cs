using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Project.Create;
using TapTrackAPI.Core.Features.Project.Delete;
using TapTrackAPI.Core.Features.Project.Edit;
using TapTrackAPI.Core.Features.Project.Get;
using TapTrackAPI.Core.Features.Project.IdVisibleUnique;
using TapTrackAPI.Core.Features.Project.List;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController : AuthorizedApiController
    {
        public ProjectController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await Mediator.Send(new GetProjectsListQuery(User)));
        }

        [HttpGet("idVisibleAvailability/{idVisible}")]
        public async Task<IActionResult> Get(string idVisible)
        {
            return Ok(await Mediator.Send(new GetUniquenessOfIdQuery(idVisible)));
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ProjectCreateCommand command)
        {
            var commandWithUser = new ProjectCreateCommand(command.Name, command.IdVisible, command.Description,
                command.Logo, User);
            var projectDto = await Mediator.Send(commandWithUser);
            return Ok(projectDto);
        }


        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject([FromForm] ProjectEditCommand command, Guid projectId)
        {
            var editCommand = new ProjectEditCommand(projectId, command.Name, command.IdVisible, command.Description,
                command.Logo, User);
            var projectDto = await Mediator.Send(editCommand);
            return Ok(projectDto);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            var getProjectByIdQuery = new GetProjectByIdQuery(projectId) {ClaimsPrincipal = User};
            return Ok(await Mediator.Send(getProjectByIdQuery));
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> Delete(Guid projectId)
        {
            return Ok(await Mediator.Send(new ProjectDeleteCommand(User, projectId)));
        }
    }
}