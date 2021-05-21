using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Features.Issue.Delete
{
    [UsedImplicitly]
    public class IssueDeleteHandler : RequestHandlerBase, IRequestHandler<IssueDeleteCommand>
    {
        public IssueDeleteHandler(DbContext dbContext, IMediator mediator, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(IssueDeleteCommand request, CancellationToken cancellationToken)
        {
            var issues = Context.Set<Entities.Issue>();
            var issue = issues.FirstOrDefault(x => x.Id == request.IssueId);

            if (issue == null)
                return Unit.Value;

            issues.Remove(issue);
            await Context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}