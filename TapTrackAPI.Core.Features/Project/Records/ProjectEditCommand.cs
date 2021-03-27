using System;
using Microsoft.AspNetCore.Http;

namespace TapTrackAPI.Core.Features.Project.Records
{
    public record ProjectEditCommand(Guid Id, string Name, string IdVisible, string Description, IFormFile Logo)
        : ProjectCreateCommand(Name, IdVisible, Description, Logo);
}