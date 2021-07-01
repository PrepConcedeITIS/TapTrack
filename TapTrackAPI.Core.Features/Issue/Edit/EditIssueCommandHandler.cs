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
    public class EditIssueCommandHandler: BaseHandler<EditIssueCommand, Guid>
    {
        public EditIssueCommandHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<Guid> Handle(EditIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = await DbContext.Set<Entities.Issue>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            issue.UpdateTitle(request.Title);
            issue.UpdateDescription(request.Description);
            if (issue.ProjectId != request.ProjectId)
            {
                issue.UpdateProject(request.ProjectId);
            }

            await DbContext.SaveChangesAsync(cancellationToken);
            
            return request.Id;
        }
    }
}