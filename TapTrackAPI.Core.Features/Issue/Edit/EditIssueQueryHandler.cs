using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class IssueEditQueryHandler: RequestHandlerBase, IRequestHandler<EditIssueQuery, EditIssueDto>
    {
        public IssueEditQueryHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Task<EditIssueDto> Handle(EditIssueQuery request, CancellationToken cancellationToken)
        {
            var issue = Context.Set<Entities.Issue>()
                .Where(x => x.Id == request.Id)
                .ProjectTo<EditIssueDto>(Mapper.ConfigurationProvider)
                .FirstOrDefault();
            return Task.FromResult(issue);
        }
    }
}