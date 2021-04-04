using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record UpdateProfileImageCommand(IFormFile Image, ClaimsPrincipal ClaimsPrincipal) : 
        RecordBase<bool>(ClaimsPrincipal);
}