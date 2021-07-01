using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class EditPriorityIssueHandler : BaseHandler<EditPriorityIssueCommand, Guid>
    {
        public EditPriorityIssueHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Guid> Handle(EditPriorityIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = DbContext.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdatePriority(request.Priority);
            await DbContext.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}
