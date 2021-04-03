using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Base.Handlers;

namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class GetIssueListHandler : IAsyncQueryHandler<GetIssueQuery, List<IssueListDto>>
    {
        private readonly DbContext _dbContext;

        public GetIssueListHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<IssueListDto>> Handle(GetIssueQuery input)
        {
            // var issues = _dbContext.Set<Entities.Issue>()
            //     .Select(x => new IssueListDto()
            //     {
            //         
            //     })
            //     .ToList();
            var issues = new List<IssueListDto>();
            return Task.FromResult(issues);
        }
    }
}