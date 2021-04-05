using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record GetUserProjectsQuery(ClaimsPrincipal ClaimsPrincipal) : 
        RecordBase<UserProjectsDto>(ClaimsPrincipal);
}