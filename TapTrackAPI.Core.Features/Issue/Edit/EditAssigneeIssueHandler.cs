using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditAssigneeIssueHandler: RequestHandlerBase, IRequestHandler<EditAssigneeIssueCommand, Guid>
    {
        public EditAssigneeIssueHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> Handle(EditAssigneeIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = Context.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            var teamMember = await Context.Set<TeamMember>()
                .FirstOrDefaultAsync(x => x.User.UserName == request.Assignee, cancellationToken);
            issue.UpdateAssignee(teamMember?.Id);
            await Context.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}