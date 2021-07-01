using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Delete
{
    [UsedImplicitly]
    public class DeleteArticleCommandHandler : BaseHandlerWithUserManager<DeleteArticleCommand, Unit>
    {
        public DeleteArticleCommandHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager) : base(
            dbContext, mapper, userManager)
        {
        }

        public override async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
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