using System.Collections.Generic;

namespace TapTrackAPI.Core.Features.Profile.Records.Dtos
{
    public record ContactInformationDto(Dictionary<string, string> Contacts);
}