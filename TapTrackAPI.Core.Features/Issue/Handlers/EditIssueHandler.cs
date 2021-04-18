using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Features.Issue.Queries;

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
            var issues = _dbContext.Set<Entities.Issue>();
            var issue = await issues.FindAsync(request.Id);
            var assignee = await _dbContext.Set<TeamMember>().FirstOrDefaultAsync(
                x => x.User.UserName == request.Assignee,
                cancellationToken: cancellationToken);
            var project = await _dbContext.Set<Entities.Project>()
                .FirstOrDefaultAsync(x => x.Name == request.Project, cancellationToken: cancellationToken);
            issue.Update(request.Title, request.Description, assignee, project, request.Estimation, request.Spent,
                request.State, request.IssueType, request.Priority);
            issues.Update(issue);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return issue;
        }
    }
}