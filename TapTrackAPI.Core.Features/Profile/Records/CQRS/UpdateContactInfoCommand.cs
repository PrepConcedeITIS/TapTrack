using System.Collections.Generic;
using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record UpdateContactInfoCommand(List<ContactInformationDto> Contacts, ClaimsPrincipal ClaimsPrincipal = null) :
        RecordBase<ContactInformationDto>(
            ClaimsPrincipal);
}