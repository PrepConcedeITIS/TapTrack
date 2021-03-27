using System;
using System.Threading.Tasks;
using Force.Cqrs;

namespace TapTrackAPI.Core.Features.Project.Records
{
    public record GetProjectByIdQuery(Guid ProjectId) : IQuery<Task<ProjectDto>>;
}