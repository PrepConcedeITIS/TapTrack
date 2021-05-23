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
    public class EditIssueCommandHandler: RequestHandlerBase, IRequestHandler<EditIssueCommand, Guid>
    {
        public EditIssueCommandHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> Handle(EditIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = await Context.Set<Entities.Issue>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            issue.Update(request.Title, request.Description);
            if (issue.ProjectId != request.ProjectId)
            {
                issue.UpdateProject(request.ProjectId);
            }

            await Context.SaveChangesAsync(cancellationToken);
            
            return request.Id;
        }
    }
}