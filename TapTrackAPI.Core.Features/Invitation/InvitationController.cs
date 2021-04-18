using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class InvitationController : AuthorizedApiController
    {
        public InvitationController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpGet("GetInvitedUsers")]
        public async Task<IActionResult> GetInvitedUsers(GetInvitedUserQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        
        [HttpPost("Invite")]
        public async Task<IActionResult> InviteUser(InviteUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}