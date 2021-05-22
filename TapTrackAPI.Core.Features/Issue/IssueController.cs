using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Issue.Create;
using TapTrackAPI.Core.Features.Issue.Delete;
using TapTrackAPI.Core.Features.Issue.Edit;
using TapTrackAPI.Core.Features.Issue.Get;
using TapTrackAPI.Core.Features.Issue.Services;

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
        public async Task<ActionResult<Entities.Issue>> Get([FromRoute] GetIssueQuery query) =>
            Ok(await Mediator.Send(query));
        

        [HttpGet("Edit/{issueId}")]
        public async Task<ActionResult<EditIssueDto>> Get([FromRoute] Guid issueId) => 
            Ok(await Mediator.Send(new EditIssueQuery(issueId)));
        
        [HttpPost("Edit/{issueId}")]
        public async Task<IActionResult> Edit([FromForm] EditIssueCommand command, [FromRoute] Guid issueId) => 
            Ok(await Mediator.Send(command with {Id = issueId}));

        [HttpPost("Create/{projectId}")]
        public async Task<IActionResult> Create([FromForm] CreateIssueCommand command, [FromRoute] Guid projectId)
        {
            var commandWithUser = new CreateIssueCommand(command.Name, command.Description, projectId, User);
            return Ok(await Mediator.Send(commandWithUser));
        }

        [HttpGet("board/{ProjectId}")]
        public async Task<ActionResult<IssueOnBoardDto>> GetByProjectId([FromRoute] GetIssuesByProjectIdQuery query) => 
            Ok(await Mediator.Send(query));

        [HttpPut("priority")]
        public async Task<IActionResult> EditPriority([FromBody] EditPriorityIssueCommand command) => 
            Ok(await Mediator.Send(command));

        [HttpPut("state")]
        public async Task<IActionResult> EditState([FromBody] EditStateIssueCommand command) => 
            Ok(await Mediator.Send(command));

        [HttpPut("assignee")]
        public async Task<IActionResult> EditAssignee([FromBody] EditAssigneeIssueCommand command) => 
            Ok(await Mediator.Send(command));

        [HttpPut("issueType")]
        public async Task<IActionResult> EditIssueType([FromBody] EditIssueTypeCommand command) => 
            Ok(await Mediator.Send(command));
        [HttpPut("spent")]
        public async Task<IActionResult> EditIssueSpentTime([FromBody] EditIssueSpentTimeCommand command) => 
            Ok(await Mediator.Send(command));
        [HttpPut("estimation")]
        public async Task<IActionResult> EditIssueEstimationTime([FromBody] EditIssueEstimationTimeCommand command) => 
            Ok(await Mediator.Send(command));

        [HttpGet("Dropdowns/{projectId}")]
        public IActionResult GetIssueDetailDropdownsSchema([FromRoute] Guid projectId)
        {
            var schema = _issueDetailsDropdownsSchemaService.GetSchema(projectId);
            return schema == null ? BadRequest() : Ok(schema);
        }
        
        [HttpDelete("{issueId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid issueId) => 
            Ok(await Mediator.Send(new IssueDeleteCommand(issueId)));
    }
}