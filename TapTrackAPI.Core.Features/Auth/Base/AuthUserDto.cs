using System;

namespace TapTrackAPI.Core.Features.Auth.Base
{
    public record AuthUserDto(Guid Id, string Email);
}