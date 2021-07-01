using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    [UsedImplicitly]
    public class GetIssuesByProjectIdHandler : BaseHandler<GetIssuesByProjectIdQuery, List<IssueOnBoardDto>>
    {
        public GetIssuesByProjectIdHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<List<IssueOnBoardDto>> Handle(GetIssuesByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var issueList = await DbContext
                .Set<Entities.Issue>()
                .Where(x => x.ProjectId == request.ProjectId)
                .ProjectTo<IssueOnBoardDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return issueList;
        }
    }
}
