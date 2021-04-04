using System.Collections.Generic;

namespace TapTrackAPI.Core.Features.Profile.Records.Dtos
{
    public record GetUserProjectsDto(Dictionary<string, string> ProjectPosition);
}