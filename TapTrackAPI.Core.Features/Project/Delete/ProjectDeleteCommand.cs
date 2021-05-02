using System;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Features.Project.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Delete
{
    public class ProjectDeleteCommand : IRequest, IHasClaims, IHasProjectId
    {
        public ProjectDeleteCommand(ClaimsPrincipal claimsPrincipal, Guid projectId)
        {
            ClaimsPrincipal = claimsPrincipal;
            ProjectId = projectId;
        }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public Guid ProjectId { get; init; }
    }
}