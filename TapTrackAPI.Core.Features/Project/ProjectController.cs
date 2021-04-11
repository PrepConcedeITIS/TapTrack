using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Project.Create;
using TapTrackAPI.Core.Features.Project.Edit;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectController : AuthorizedApiController
    {
        private readonly IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> _getUniquenessQueryHandler;
        private readonly IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto> _getProjectByIdHandler;

        public ProjectController(IAsyncQueryHandler<GetUniquenessOfIdQuery, bool> getUniquenessQueryHandler,
            IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto> getProjectByIdHandler,
            IMediator mediator)
            : base(mediator)
        {
            _getUniquenessQueryHandler = getUniquenessQueryHandler;
            _getProjectByIdHandler = getProjectByIdHandler;
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

        #region Create

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ProjectCreateCommand command)
        {
            var commandWithUser = command with {ClaimsPrincipal = User};
            var projectDto = await Mediator.Send(commandWithUser);
            return Ok(projectDto);
        }

        #endregion

        #region Edit

        [HttpPut("{projectId}/edit")]
        public async Task<IActionResult> UpdateProject([FromForm] ProjectEditCommand command, Guid projectId)
        {
            var editCommand = command with {ClaimsPrincipal = User, ProjectId = projectId};
            var projectDto = await Mediator.Send(editCommand);
            return Ok(projectDto);
        }

        [HttpGet("{projectId}/edit")]
        public async Task<IActionResult> GetProjectForEdit(Guid projectId)
        {
            var result = await _getProjectByIdHandler.Handle(new GetProjectByIdQuery(projectId));
            return Ok(result);
        }

        #endregion
    }
}