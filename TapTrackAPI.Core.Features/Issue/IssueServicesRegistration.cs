using Microsoft.Extensions.DependencyInjection;
using TapTrackAPI.Core.Features.Issue.Base;
using TapTrackAPI.Core.Features.Issue.Services;

namespace TapTrackAPI.Core.Features.Issue
{
    public static class IssueServicesRegistration
    {
        public static IServiceCollection AddIssueServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IssueDetailsDropdownsSchemaService>()
                .AddScoped<IIssueBuilder, IssueBuilder>();
        }
    }
}