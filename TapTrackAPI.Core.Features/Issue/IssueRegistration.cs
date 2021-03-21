using System.Collections.Generic;
using System.Threading.Tasks;
using Force.Cqrs;
using Microsoft.Extensions.DependencyInjection;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Features.Issue.Handlers;

namespace TapTrackAPI.Core.Features.Issue
{
    public static class IssueRegistration
    {
        public static void RegisterIssue(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IQueryHandler<GetIssueQuery, Task<List<IssueListDto>>>, GetIssueListHandler>();
        }
    }
}