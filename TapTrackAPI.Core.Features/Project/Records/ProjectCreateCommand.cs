using System.Threading.Tasks;
using Force.Cqrs;
using Microsoft.AspNetCore.Http;

namespace TapTrackAPI.Core.Features.Project.Records
{
    public record ProjectCreateCommand(string Name, string IdVisible, string Description, IFormFile Logo)
        : ICommand<Task<ProjectDto>>;
}