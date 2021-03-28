using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Base.Handlers;
using Force.Cqrs;
using TapTrackAPI.Core.Enums;

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
            _dbContext.Set<Entities.Issue>().Add(new Entities.Issue("Title", 
                "Description", 
                new Guid("1"), 
                new Guid("1"), 
                new Guid("a85238d7-fc93-43f6-bdde-e8574a603c0b"),
                IssueType.Task,
                Priority.Normal));
            _dbContext.SaveChanges();
            
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