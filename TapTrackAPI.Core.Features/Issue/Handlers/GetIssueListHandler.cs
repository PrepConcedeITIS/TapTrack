using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Issue.Dtos;
using TapTrackAPI.Core.Features.Issue.Queries;


namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class GetIssueListHandler : RequestHandlerBase,
        IRequestHandler<GetListIssueQuery, List<IssueListDto>>
    {
        private readonly DbContext _dbContext;

        public GetIssueListHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public Task<List<IssueListDto>> Handle(GetListIssueQuery input, CancellationToken cancellationToken)
        {
            var issues = _dbContext
                .Set<Entities.Issue>()
                .ProjectTo<IssueListDto>(Mapper.ConfigurationProvider)
                .ToList();
            return Task.FromResult(issues);
        }
    }
}