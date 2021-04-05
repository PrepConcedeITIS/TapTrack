using System.Collections.Generic;

namespace TapTrackAPI.Core.Features.Profile.Records.Dtos
{
    public record UserProjectsDto(Dictionary<string, string> ProjectPosition);
}