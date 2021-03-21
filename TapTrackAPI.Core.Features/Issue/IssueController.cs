using System.Collections.Generic;
using System.Threading.Tasks;
using Force.Cqrs;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue
{
    public class IssueController : AuthorizedApiController
    {
        private readonly IQueryHandler<GetIssueQuery, Task<List<IssueListDto>>> _getIssueHandler;

        public IssueController(IQueryHandler<GetIssueQuery, Task<List<IssueListDto>>> getIssueHandler)
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