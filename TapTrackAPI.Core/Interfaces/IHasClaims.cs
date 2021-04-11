using System.Security.Claims;

namespace TapTrackAPI.Core.Interfaces
{
    public interface IHasClaims
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}