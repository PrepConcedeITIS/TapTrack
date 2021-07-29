using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Invitation.Dto;
using TapTrackAPI.Core.Features.Invitation.GetInvitationResults;
using TapTrackAPI.Core.Features.Invitation.GetInvitationsByUser;
using TapTrackAPI.Core.Features.Invitation.GetUnResolvedInvitesCountByUser;
using TapTrackAPI.Core.Features.Invitation.InviteUser;
using TapTrackAPI.Core.Features.Invitation.ResolveInvitation;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class InvitationController : AuthorizedApiController
    {
        public InvitationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetInvitedUsers/{projectId}")]
        public async Task<IActionResult> GetInvitedUsers([FromRoute] Guid projectId)
        {
            var query = new GetInvitationResultsQuery(projectId);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("user")]
        public async Task<ActionResult<InvitationDtoDetailed[]>> GetUnResolvedInvitesByUser()
        {
            var result = await Mediator.Send(new GetInvitationsByUserQuery(User));
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetUnResolvedInvitesCountByUser()
        {
            var result = await Mediator.Send(new GetInvitationsByUserCountQuery(User));
            return Ok(result);
        }

        [HttpPost("Invite")]
        public async Task<IActionResult> InviteUser([FromBody] InviteUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("ResolveInvitation")]
        public async Task<IActionResult> ResolveInvitation([FromBody] ResolveInvitationCommand command)
        {
            return Ok(await Mediator.Send(command with {ClaimsPrincipal = User}));
        }
    }
}