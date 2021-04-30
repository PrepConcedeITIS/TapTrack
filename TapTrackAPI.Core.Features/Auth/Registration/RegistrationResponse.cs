using Microsoft.AspNetCore.Identity;
using TapTrackAPI.Core.Features.Auth.Base;

namespace TapTrackAPI.Core.Features.Auth.Registration
{
    public record RegistrationResponse(AuthUserDto User, string Token, IdentityError[] Errors, UserInput Input);
}