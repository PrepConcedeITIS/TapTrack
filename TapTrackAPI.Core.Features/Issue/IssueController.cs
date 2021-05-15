using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Issue.Delete;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Features.Issue.Queries;

namespace TapTrackAPI.Core.Features.Issue
{
    public class IssueController : AuthorizedApiController
    {
        private readonly IssueDetailsDropdownsSchemaService _issueDetailsDropdownsSchemaService;
        public IssueController(IMediator mediator, 
            IssueDetailsDropdownsSchemaService issueDetailsDropdownsSchemaService) : base(mediator)
        {
            _issueDetailsDropdownsSchemaService = issueDetailsDropdownsSchemaService;
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

        [HttpGet("board/{ProjectId}")]
        public async Task<ActionResult<IssueOnBoardDto>> GetByProjectId([FromRoute] GetIssuesByProjectIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPut("priority")]
        public async Task<IActionResult> EditPriority([FromBody] EditPriorityIssueCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("state")]
        public async Task<IActionResult> EditState([FromBody] EditStateIssueCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("Dropdowns/{issueId}")]
        public IActionResult GetIssueDetailDropdownsSchema([FromRoute] Guid issueId)
        {
            var schema = _issueDetailsDropdownsSchemaService.GetSchema(issueId);
            return schema == null ? BadRequest() : Ok(schema);
        }
        [HttpDelete("{issueId}")]
        public async Task<IActionResult> Delete(Guid issueId)
        {
            return Ok(await Mediator.Send(new IssueDeleteCommand(User, issueId)));
        }
    }
}