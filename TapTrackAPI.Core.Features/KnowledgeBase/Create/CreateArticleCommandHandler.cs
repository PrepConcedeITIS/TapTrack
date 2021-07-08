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
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Create
{
    [UsedImplicitly]
    public class CreateArticleCommandHandler : BaseHandlerWithUserManager<CreateArticleCommand, Guid>
    {

        public CreateArticleCommandHandler(DbContext context, IMapper mapper, UserManager<User> userManager) 
            : base(context, mapper, userManager)
        {
        }

        public override async Task<Guid> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.AppUser);
            var teamMember = await DbContext
                .Set<TeamMember>()
                .Where(x => x.ProjectId == request.BelongsToId)
                .SingleAsync(member => member.UserId == userId, cancellationToken);
            var time = DateTime.UtcNow;
            var article = new Article(request.BelongsToId, request.Title, teamMember.Id, time, teamMember.Id, time,
                request.Content);
            await DbContext
                .Set<Article>()
                .AddAsync(article, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return article.Id;
        }
    }
}