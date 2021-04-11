using System.Security.Claims;
using System.Threading.Tasks;
using Force.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Create
{
    public record ProjectCreateCommand(string Name, string IdVisible, string Description, IFormFile Logo)
        : IHasClaims, IRequest<ProjectDto>,
            ICommand<Task<ProjectDto>>
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}