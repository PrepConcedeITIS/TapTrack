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
    public class EditIssueEstimationTimeCommandHandler: RequestHandlerBase, IRequestHandler<EditIssueEstimationTimeCommand, Guid>
    {
        public EditIssueEstimationTimeCommandHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> Handle(EditIssueEstimationTimeCommand request, CancellationToken cancellationToken)
        {
            var estimation = TimeSpanFormatter.FormatterFromString(request.Estimation);
            var issues = Context.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdateEstimation(estimation);
            await Context.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}