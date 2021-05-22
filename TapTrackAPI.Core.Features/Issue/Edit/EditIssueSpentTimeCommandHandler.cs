using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Utility;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditIssueSpentTimeCommandHandler: RequestHandlerBase, IRequestHandler<EditIssueSpentTimeCommand, Guid>
    {
        public EditIssueSpentTimeCommandHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> Handle(EditIssueSpentTimeCommand request, CancellationToken cancellationToken)
        {
            var spentTime = TimeSpanFormatter.FormatterFromString(request.Spent);
            var issues = Context.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdateEstimation(spentTime);
            await Context.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}