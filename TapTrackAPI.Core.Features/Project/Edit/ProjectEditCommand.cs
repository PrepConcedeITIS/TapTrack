using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Features.Project.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    public class ProjectEditCommand : ProjectCommandBase, IRequest<ProjectDto>, IHasClaims, IHasProjectId
    {
        public ProjectEditCommand()
        {
            
        }
        public ProjectEditCommand(Guid projectId, string name, string idVisible, string description, IFormFile logo, ClaimsPrincipal claimsPrincipal)
        {
            ProjectId = projectId;
            Name = name;
            IdVisible = idVisible;
            Description = description;
            Logo = logo;
            ClaimsPrincipal = claimsPrincipal;
        }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public Guid ProjectId { get; init; }
        public IFormFile Logo { get; set; }
    }

    public abstract class ProjectCommandBase
    {
        public ProjectCommandBase()
        {
            
        }
        public string Name { get; init; }
        public string IdVisible { get; init; }
        public string Description { get; init; }

    }
}