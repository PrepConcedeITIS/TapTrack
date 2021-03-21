using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.Issue.Dtos;
using Force.Cqrs;

namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class GetIssueListHandler : IQueryHandler<GetIssueQuery, Task<List<IssueListDto>>>
    {
        private readonly DbContext _dbContext;

        public GetIssueListHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<IssueListDto>> Handle(GetIssueQuery input)
        {
            var issues = _dbContext.Set<Entities.Issue>()
                .Select(x => new IssueListDto()
                {
                    
                })
                .ToList();
            return Task.FromResult(issues);
        }
    }
}