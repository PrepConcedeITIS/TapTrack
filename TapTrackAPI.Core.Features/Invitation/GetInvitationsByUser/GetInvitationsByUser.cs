using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Features.Invitation.Dto;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationsByUser
{
    public record GetInvitationsByUserQuery: IRequest<InvitationDtoDetailed[]>, IHasClaims
    {
        public GetInvitationsByUserQuery(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}