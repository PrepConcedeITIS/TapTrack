using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class EditPriorityIssueHandler : RequestHandlerBase, IRequestHandler<EditPriorityIssueCommand, Guid>
    {
        public EditPriorityIssueHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Guid> Handle(EditPriorityIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = Context.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdatePriority(request.Priority);
            await Context.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}
