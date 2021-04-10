using System.Security.Claims;

namespace TapTrackAPI.Core.Base
{
    public record CommandBase(ClaimsPrincipal Claims);
}