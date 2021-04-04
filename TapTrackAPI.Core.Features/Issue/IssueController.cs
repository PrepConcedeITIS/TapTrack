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
        private readonly IAsyncQueryHandler<GetListIssueQuery, List<IssueListDto>> _getListIssueHandler;

        public IssueController(IAsyncQueryHandler<GetListIssueQuery, List<IssueListDto>> getIssueHandler,
            IMediator mediator)
            : base(mediator)
        {
            _getListIssueHandler = getIssueHandler;
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueListDto>>> Get([FromQuery] GetListIssueQuery query)
        {
            var issues = await _getListIssueHandler.Handle(query);
            return Ok(issues);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Entities.Issue>> Get([FromRoute] GetIssueQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
