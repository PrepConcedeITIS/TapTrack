using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record GetUserProfileQuery(ClaimsPrincipal ClaimsPrincipal) : 
        RecordBase<GetUserProfileDto>(ClaimsPrincipal);
}