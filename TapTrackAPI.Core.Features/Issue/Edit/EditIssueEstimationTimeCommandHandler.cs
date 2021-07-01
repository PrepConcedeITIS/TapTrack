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
    public class EditIssueEstimationTimeCommandHandler: BaseHandler<EditIssueEstimationTimeCommand, Guid>
    {
        public EditIssueEstimationTimeCommandHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<Guid> Handle(EditIssueEstimationTimeCommand request, CancellationToken cancellationToken)
        {
            var estimation = TimeSpanFormatter.FormatterFromString(request.Estimation);
            var issues = DbContext.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdateEstimation(estimation);
            await DbContext.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}