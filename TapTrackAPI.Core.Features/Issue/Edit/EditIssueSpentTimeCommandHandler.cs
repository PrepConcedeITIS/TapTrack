using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Base.Utility;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class EditIssueSpentTimeCommandHandler: BaseHandler<EditIssueSpentTimeCommand, Guid>
    {
        public EditIssueSpentTimeCommandHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<Guid> Handle(EditIssueSpentTimeCommand request, CancellationToken cancellationToken)
        {
            var spentTime = TimeSpanFormatter.FormatterFromString(request.Spent);
            var issues = DbContext.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdateSpentTime(spentTime);
            await DbContext.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}