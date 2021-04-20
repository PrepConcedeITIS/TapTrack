using System.Collections.Generic;
using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    public record UpdateContactInfoCommand(List<ContactInformationDto> Contacts) :
        ProfileRecordBase<List<ContactInformationDto>>;
}