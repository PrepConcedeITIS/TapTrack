using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public class GetIssueHandler : RequestHandlerBase,
         IRequestHandler<GetIssueQuery, IssueDetailsDto>
    {
        public GetIssueHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public Task<IssueDetailsDto> Handle(GetIssueQuery request, CancellationToken cancellationToken)
        {
            var issue = Context.Set<Entities.Issue>()
                .Where(x => x.Id == request.Id)
                .ProjectTo<IssueDetailsDto>(Mapper.ConfigurationProvider)
                .FirstOrDefault();
            return Task.FromResult(issue);
        }
    }
}