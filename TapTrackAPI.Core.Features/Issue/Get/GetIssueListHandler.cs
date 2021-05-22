using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public class GetIssueListHandler : RequestHandlerBase,
        IRequestHandler<GetIssueListQuery, List<IssueListItemDto>>
    {
        private readonly DbContext _dbContext;

        public GetIssueListHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<List<IssueListItemDto>> Handle(GetIssueListQuery input, CancellationToken cancellationToken)
        {
            var issues = await _dbContext
                .Set<Entities.Issue>()
                .ProjectTo<IssueListItemDto>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return issues;
        }
    }
}