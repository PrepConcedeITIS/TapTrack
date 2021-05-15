using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Issue.Queries;

namespace TapTrackAPI.Core.Features.Issue.Handlers
{
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
