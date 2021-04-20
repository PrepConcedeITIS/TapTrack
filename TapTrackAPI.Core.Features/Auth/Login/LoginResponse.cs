using TapTrackAPI.Core.Features.Auth.Base;

namespace TapTrackAPI.Core.Features.Auth.Login
{
    public record LoginResponse(AuthUserDto User, string Token);
}