using Microsoft.Extensions.DependencyInjection;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Project.Handlers;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project
{
    public static class ProjectRegistration
    {
        public static void RegisterProject(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAsyncQueryHandler<GetUniquenessOfIdQuery, bool>, GetUniquenessOfIdQueryHandler>();
            serviceCollection.AddScoped<IAsyncCommandHandler<ProjectEditCommand, ProjectDto>, UpdateProjectInfoAsyncHandler>();
            serviceCollection.AddScoped<IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto>, GetProjectByIdAsyncQueryHandler>();
        }
    }
}