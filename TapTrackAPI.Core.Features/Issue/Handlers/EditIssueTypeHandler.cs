using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Issue.Queries;

namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class EditIssueTypeHandler: RequestHandlerBase, IRequestHandler<EditIssueTypeCommand, Guid>
    {
        public EditIssueTypeHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> Handle(EditIssueTypeCommand request, CancellationToken cancellationToken)
        {
            var issues = Context.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdateIssueType(request.IssueType);
            await Context.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}