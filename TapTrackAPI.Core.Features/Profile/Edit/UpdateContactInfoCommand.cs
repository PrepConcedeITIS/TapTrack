using System.Collections.Generic;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    public record UpdateContactInfoCommand(List<ContactInformationListItemDto> Contacts) :
        ProfileRecordBase<List<ContactInformationListItemDto>>;
}