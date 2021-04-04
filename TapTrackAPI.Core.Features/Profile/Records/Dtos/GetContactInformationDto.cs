using System.Collections.Generic;

namespace TapTrackAPI.Core.Features.Profile.Records.Dtos
{
    public record GetContactInformationDto(Dictionary<string, string> Contacts);
}