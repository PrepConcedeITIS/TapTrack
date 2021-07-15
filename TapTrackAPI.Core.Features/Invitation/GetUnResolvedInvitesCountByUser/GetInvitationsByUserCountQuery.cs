using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Invitation.GetUnResolvedInvitesCountByUser
{
    public record GetInvitationsByUserCountQuery: IRequest<int>, IHasClaims
    {
        public GetInvitationsByUserCountQuery(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}