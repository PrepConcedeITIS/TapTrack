using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public class GetIssuesByProjectIdHandler : RequestHandlerBase,
         IRequestHandler<GetIssuesByProjectIdQuery, List<IssueOnBoardDto>>
    {
        private readonly DbContext _dbContext;

        public GetIssuesByProjectIdHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<List<IssueOnBoardDto>> Handle(GetIssuesByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var issueList = await _dbContext
                .Set<Entities.Issue>()
                .Where(x => x.ProjectId == request.ProjectId)
                .ProjectTo<IssueOnBoardDto>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return issueList;
        }
    }
}
