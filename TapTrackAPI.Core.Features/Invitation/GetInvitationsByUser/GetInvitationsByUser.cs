using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationsByUser
{
    public record GetInvitationsByUserQuery: IRequest<InvitationDto[]>, IHasClaims
    {
        public GetInvitationsByUserQuery(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}