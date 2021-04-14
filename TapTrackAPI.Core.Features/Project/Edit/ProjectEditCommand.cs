using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Features.Project.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    public record ProjectEditCommand(Guid ProjectId, string Name, string IdVisible, string Description, IFormFile Logo)
        : IRequest<ProjectDto>, IHasClaims, IHasProjectId
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}