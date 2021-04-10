using System.Security.Claims;
using System.Threading.Tasks;
using Force.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Features.Project.Records
{
    public record ProjectCreateCommand(string Name, string IdVisible, string Description, IFormFile Logo, ClaimsPrincipal Claims = null)
        : CommandBase(Claims), IRequest<ProjectDto>,
            ICommand<Task<ProjectDto>>;
}