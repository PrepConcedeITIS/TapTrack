using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue
{
    public class IssueController : AuthorizedApiController
    {
        private readonly IAsyncQueryHandler<GetIssueQuery, List<IssueListDto>> _getIssueHandler;

        public IssueController(IAsyncQueryHandler<GetIssueQuery, List<IssueListDto>> getIssueHandler)
        {
            _getIssueHandler = getIssueHandler;
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueListDto>>> Get([FromQuery] GetIssueQuery query)
        {
            var issues = await _getIssueHandler.Handle(query);
            return Ok(issues);
        }
    }
}