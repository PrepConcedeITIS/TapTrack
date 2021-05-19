﻿using AutoMapper;
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
    public class EditPriorityIssueHandler : RequestHandlerBase, IRequestHandler<EditPriorityIssueCommand, Guid>
    {
        private readonly IMediator _mediator;

        public EditPriorityIssueHandler(DbContext dbContext, IMediator mediator, IMapper mapper) : base(dbContext, mapper)
        {
            _mediator = mediator;
        }

        public async Task<Guid> Handle(EditPriorityIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = Context.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            issue.UpdatePriority(request.Priority);
            await Context.SaveChangesAsync(cancellationToken);
            return issue.Id;
        }
    }
}