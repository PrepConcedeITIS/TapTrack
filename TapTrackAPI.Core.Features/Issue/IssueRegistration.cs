using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Features.Issue.Handlers;

namespace TapTrackAPI.Core.Features.Issue
{
    public static class IssueRegistration
    {
        public static void RegisterIssue(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAsyncQueryHandler<GetIssueQuery, List<IssueListDto>>, GetIssueListHandler>();
        }
    }
}