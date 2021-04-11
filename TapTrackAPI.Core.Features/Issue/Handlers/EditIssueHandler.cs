using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class EditIssueHandler : IRequestHandler<EditIssueCommand, Entities.Issue>
    {
        private readonly DbContext _dbContext;

        public EditIssueHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entities.Issue> Handle(EditIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = await _dbContext.Set<Entities.Issue>().FindAsync(request.Id);
            var assignee = await _dbContext.Set<TeamMember>().FirstOrDefaultAsync(
                x => x.User.UserName == request.Assignee,
                cancellationToken: cancellationToken);
            var project = await _dbContext.Set<Entities.Project>().FirstOrDefaultAsync(x => x.Name == request.Project);
            issue.Update(request.Title, request.Description, assignee, project, request.Estimation, request.Spent,
                request.State, request.IssueType, request.Priority);
            _dbContext.Set<Entities.Issue>().Update(issue);
            await _dbContext.SaveChangesAsync();
            return issue;
        }
    }
}