using System;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Features.Project.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Get
{
    public record GetProjectByIdQuery(Guid ProjectId) : IRequest<ProjectDto>, IHasClaims, IHasProjectId
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}