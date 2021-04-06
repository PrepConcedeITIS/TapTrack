using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue
{
    public class IssueController : AuthorizedApiController
    {
        public IssueController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueListDto>>> Get([FromQuery] GetListIssueQuery query)
            => Ok(await Mediator.Send(query));

        [HttpGet("{Id}")]
        public async Task<ActionResult<Entities.Issue>> Get([FromRoute] GetIssueQuery query)
            => Ok(await Mediator.Send(query));
    }
}
