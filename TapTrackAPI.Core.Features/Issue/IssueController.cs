using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Features.Issue.Queries;

namespace TapTrackAPI.Core.Features.Issue
{
    public class IssueController : AuthorizedApiController
    {
        public IssueController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueListItemDto>>> Get([FromQuery] GetIssueListQuery query)
            => Ok(await Mediator.Send(query));

        [HttpGet("{Id}")]
        public async Task<ActionResult<Entities.Issue>> Get([FromRoute] GetIssueQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> Edit([FromQuery] EditIssueCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}