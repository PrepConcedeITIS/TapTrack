using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Enums;


namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class GetIssueListHandler : IRequestHandler<GetListIssueQuery, List<IssueListDto>>
    {
        private readonly DbContext _dbContext;

        public GetIssueListHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<IssueListDto>> Handle(GetListIssueQuery input, CancellationToken cancellationToken)
        {
            var issues = _dbContext.Set<Entities.Issue>()
                .Select(x => new IssueListDto(x.Title, 
                    x.Project.Name,
                    x.Priority.ToString(),
                    x.State.ToString(),
                    x.Creator.User.UserName,
                    x.Assignee.User.UserName,
                    x.Estimation.ToString("c"),
                    x.Spent.ToString("c")))
                .ToList();
            return Task.FromResult(issues);
        }
    }
}