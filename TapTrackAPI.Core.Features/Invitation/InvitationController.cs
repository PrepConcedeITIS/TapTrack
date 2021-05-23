using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Invitation.GetInvitationResults;
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
        public async Task<IActionResult> GetInvitedUsers([FromBody] Guid projectId)
        {
            var query = new GetInvitationResultsQuery(projectId);
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("Invite")]
        public async Task<IActionResult> InviteUser([FromBody] InviteUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("AcceptOrDeclineInvitation")]
        public async Task<IActionResult> AcceptOrDeclineInvitation([FromQuery] ResolveInvitationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}