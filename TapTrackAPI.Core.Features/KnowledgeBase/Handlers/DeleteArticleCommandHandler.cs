using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.KnowledgeBase.Base;
using TapTrackAPI.Core.Features.KnowledgeBase.Commands;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Handlers
{
    public class DeleteArticleCommandHandler : BaseCommandHandler, IRequestHandler<DeleteArticleCommand, Unit>
    {
        public DeleteArticleCommandHandler(DbContext dbContext, UserManager<User> userManager) : base(dbContext,
            userManager)
        {
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await DbContext
                .Set<Article>()
                .Include(x => x.Comments)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            DbContext.Entry(article).State = EntityState.Deleted;
            DbContext
                .Set<Comment>()
                .RemoveRange(article.Comments);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}