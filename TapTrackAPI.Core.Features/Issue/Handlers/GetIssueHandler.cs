using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class GetIssueHandler : IRequestHandler<GetIssueQuery, IssueDetailsDto>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public GetIssueHandler(DbContext dbContext, IMapper _mapper)
        {
            _dbContext = dbContext;
            this._mapper = _mapper;
        }

        public Task<IssueDetailsDto> Handle(GetIssueQuery request, CancellationToken cancellationToken)
        {
            var issue = _dbContext.Set<Entities.Issue>()
                .Where(x => x.Id == request.Id)
                .Select(i =>
                    new IssueDetailsDto(i.Title, i.Description, i.Creator.User.UserName,
                        i.Assignee.User.UserName, i.IssueType.ToString(), i.Priority.ToString(), i.Project.Name,
                        i.State.ToString(), i.Estimation.Hours, i.Estimation.Minutes,
                        i.Created.Date.ToShortDateString(), i.Spent.Hours, i.Spent.Minutes)
                )
                .FirstOrDefault();
            return Task.FromResult(issue);
        }
    }
}