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
using TapTrackAPI.Core.Features.Issue.Delete;


namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class DeleteIssueHandler : RequestHandlerBase, IRequestHandler<IssueDeleteCommand>
    {
        private readonly IMediator _mediator;
        private readonly DbContext _dbContext;


        public DeleteIssueHandler(DbContext dbContext, IMediator mediator, IMapper mapper) : base(dbContext, mapper)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(IssueDeleteCommand request, CancellationToken cancellationToken)
        {
            var issues = _dbContext.Set<Entities.Issue>();


            var issue = issues.FirstOrDefault(x => x.Id == request.IssueId);

            _dbContext.Set<Entities.Issue>().Remove(issue);

            return Unit.Value;
        }


    }
}
