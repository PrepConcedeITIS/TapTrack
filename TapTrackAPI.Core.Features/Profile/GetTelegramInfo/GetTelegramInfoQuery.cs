using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Profile.GetTelegramInfo
{
    public class GetTelegramInfoQuery : IRequest<TelegramInfo>, IHasClaims
    {
        public GetTelegramInfoQuery(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}