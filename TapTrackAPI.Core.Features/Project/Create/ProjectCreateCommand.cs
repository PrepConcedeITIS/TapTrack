using System.Security.Claims;
using System.Threading.Tasks;
using Force.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Features.Project.Edit;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Create
{
    public class ProjectCreateCommand : ProjectCommandBase, IHasClaims, IRequest<ProjectDto>,
        ICommand<Task<ProjectDto>>
    {
        public ProjectCreateCommand()
        {
        }

        public ProjectCreateCommand(string name, string idVisible, string description, IFormFile logo,
            ClaimsPrincipal claimsPrincipal)
        {
            Name = name;
            IdVisible = idVisible;
            Description = description;
            Logo = logo;
            ClaimsPrincipal = claimsPrincipal;
        }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public IFormFile Logo { get; set; }
    }
}