using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Features.Issue.Delete
{
    public class IssueDeleteAsyncCommandHandler: IRequestHandler<IssueDeleteCommand>
    {
        private readonly DbContext _dbContext;

        public IssueDeleteAsyncCommandHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(IssueDeleteCommand request, CancellationToken cancellationToken)
        {
            var issue = await _dbContext.Set<Entities.Issue>()
                .FirstOrDefaultAsync(x => x.Id == request.IssueId, cancellationToken);
            _dbContext.Set<Entities.Issue>().Remove(issue);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}