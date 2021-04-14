using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Project.Create;
using TapTrackAPI.Core.Features.Project.Edit;
using TapTrackAPI.Core.Features.Project.Get;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController : AuthorizedApiController
    {
        private readonly IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> _getUniquenessQueryHandler;

        public ProjectController(IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> getUniquenessQueryHandler,
            IMediator mediator)
            : base(mediator)
        {
            _getUniquenessQueryHandler = getUniquenessQueryHandler;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await Mediator.Send(new GetProjectsListQuery()));
        }

        [HttpGet("idVisibleAvailability/{idVisible}")]
        public async Task<IActionResult> Get(string idVisible)
        {
            return Ok(await _getUniquenessQueryHandler.Handle(new GetUniquenessOfIdQuery(idVisible)));
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ProjectCreateCommand command)
        {
            var commandWithUser = command with {ClaimsPrincipal = User};
            var projectDto = await Mediator.Send(commandWithUser);
            return Ok(projectDto);
        }


        [HttpPut("{projectId}/edit")]
        public async Task<IActionResult> UpdateProject([FromForm] ProjectEditCommand command, Guid projectId)
        {
            var editCommand = command with {ClaimsPrincipal = User, ProjectId = projectId};
            var projectDto = await Mediator.Send(editCommand);
            return Ok(projectDto);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectForEdit(Guid projectId)
        {
            var getProjectByIdQuery = new GetProjectByIdQuery(projectId){ClaimsPrincipal = User};
            return Ok(await Mediator.Send(getProjectByIdQuery));
        }
    }
}