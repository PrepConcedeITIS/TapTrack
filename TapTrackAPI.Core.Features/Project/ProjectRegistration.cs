using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}