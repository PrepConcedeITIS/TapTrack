using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record UpdateContactInfoCommand(string Ping, ClaimsPrincipal ClaimsPrincipal = null) :
        RecordBase<ContactInformationDto>(
            ClaimsPrincipal);
}