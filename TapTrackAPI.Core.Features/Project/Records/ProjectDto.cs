using System;

namespace TapTrackAPI.Core.Features.Project.Records
{
    public record ProjectDto(Guid Id, string Name, string Description, string IdVisible, string LogoUrl);
}