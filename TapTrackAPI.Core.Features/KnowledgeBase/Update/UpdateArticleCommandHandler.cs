using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Update
{
    [UsedImplicitly]
    public class UpdateArticleCommandHandler : BaseHandlerWithUserManager<UpdateArticleCommand, Guid>
    {
        public UpdateArticleCommandHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager) : base(
            dbContext, mapper, userManager)
        {
        }

        public override async Task<Guid> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
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