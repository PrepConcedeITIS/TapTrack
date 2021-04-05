using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record UpdateProfileImageCommand(IFormFile Image, ClaimsPrincipal ClaimsPrincipal = null) : 
        RecordBase<UserProfileDto>(ClaimsPrincipal);
}