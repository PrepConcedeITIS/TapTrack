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
    public class GetIssueHandler : BaseHandler<GetIssueQuery, IssueDetailsDto>
    {
        public GetIssueHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override Task<IssueDetailsDto> Handle(GetIssueQuery request, CancellationToken cancellationToken)
        {
            var issue = DbContext.Set<Entities.Issue>()
                .Where(x => x.Id == request.Id)
                .ProjectTo<IssueDetailsDto>(Mapper.ConfigurationProvider)
                .FirstOrDefault();
            return Task.FromResult(issue);
        }
    }
}