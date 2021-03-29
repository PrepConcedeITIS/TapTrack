using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Project.Handlers;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project
{
    public static class ProjectRegistration
    {
        public static void RegisterProject(this IServiceCollection services)
        {
            services.AddScoped<IAsyncQueryHandler<GetUniquenessOfIdQuery, bool>, GetUniquenessOfIdQueryHandler>();
            services.AddScoped<IAsyncCommandHandler<ProjectCreateCommand, ProjectDto>, CreateProjectAsyncHandler>();
            services.AddScoped<IAsyncCommandHandler<ProjectEditCommand, ProjectDto>, UpdateProjectInfoAsyncHandler>();
            services.AddScoped<IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto>, GetProjectByIdAsyncQueryHandler>();
            services.AddScoped<IAsyncQueryHandler<GetProjectsListQuery, List<ProjectDto>>, GetProjectsListAsyncHandler>(); 
        }
    }
}