using System;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Project
{
    public record ProjectDto(Guid Id, string Name, string Description, string IdVisible, string LogoUrl, Role UserRole);
}