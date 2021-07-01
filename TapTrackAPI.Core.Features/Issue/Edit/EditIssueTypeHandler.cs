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
    public class EditIssueTypeHandler: BaseHandler<EditIssueTypeCommand, Guid>
    {
        public EditIssueTypeHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<Guid> Handle(EditIssueTypeCommand request, CancellationToken cancellationToken)
        {
            var issues = DbContext.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdateIssueType(request.IssueType);
            await DbContext.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}