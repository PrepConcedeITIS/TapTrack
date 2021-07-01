using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class IssueEditQueryHandler: BaseHandler<EditIssueQuery, EditIssueDto>
    {
        public IssueEditQueryHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override Task<EditIssueDto> Handle(EditIssueQuery request, CancellationToken cancellationToken)
        {
            var issue = DbContext.Set<Entities.Issue>()
                .Where(x => x.Id == request.Id)
                .ProjectTo<EditIssueDto>(Mapper.ConfigurationProvider)
                .FirstOrDefault();
            return Task.FromResult(issue);
        }
    }
}