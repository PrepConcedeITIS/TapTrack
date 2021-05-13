using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Profile.DeleteTelegramConnection
{
    public class DeleteTelegramConnectionCommand: IRequest<Unit?>, IHasClaims
    {
        public DeleteTelegramConnectionCommand(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}