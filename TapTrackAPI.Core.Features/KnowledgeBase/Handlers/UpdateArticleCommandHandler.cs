using System;
using System.Linq;
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
    public class UpdateArticleCommandHandler : BaseCommandHandler, IRequestHandler<UpdateArticleCommand, Guid>
    {
        public UpdateArticleCommandHandler(DbContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {
        }

        public async Task<Guid> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await DbContext
                .Set<Article>()
                .FindAsync(new object[] {request.Id}, cancellationToken);
            var teamMember = await DbContext
                .Set<TeamMember>()
                .Where(x => x.ProjectId == request.BelongsToId)
                .SingleAsync(x => x.UserId == request.UserId, cancellationToken);
            article.Update(request.Title, teamMember.Id, request.Content);
            await DbContext.SaveChangesAsync(cancellationToken);
            return article.Id;
        }
    }
}