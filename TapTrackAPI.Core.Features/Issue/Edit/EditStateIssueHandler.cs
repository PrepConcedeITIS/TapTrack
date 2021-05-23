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
    public class EditStateIssueHandler : RequestHandlerBase, IRequestHandler<EditStateIssueCommand, Guid>
    {
        private readonly IMediator _mediator;

        public EditStateIssueHandler(DbContext dbContext, IMediator mediator, IMapper mapper) : base(dbContext, mapper)
        {
            _mediator = mediator;
        }

        public async Task<Guid> Handle(EditStateIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = Context.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdateState(request.State);
            await Context.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}
