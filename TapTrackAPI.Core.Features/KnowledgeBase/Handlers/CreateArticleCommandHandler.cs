using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.KnowledgeBase.Commands;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Handlers
{
    public class CreateArticleCommandHandler : RequestHandlerBase, IRequestHandler<CreateArticleCommand, Guid>
    {
        private UserManager<User> UserManager { get; }

        public CreateArticleCommandHandler(DbContext context, IMapper mapper, UserManager<User> userManager) : base(context,
            mapper)
        {
            UserManager = userManager;
        }

        public async Task<Guid> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.AppUser);
            var teamMember = await Context
                .Set<TeamMember>()
                .Where(x => x.ProjectId == request.BelongsToId)
                .SingleAsync(member => member.UserId == userId, cancellationToken);
            var time = DateTime.Now;
            var article = new Article(request.BelongsToId, request.Title, teamMember.Id, time, teamMember.Id, time,
                request.Content);
            await Context
                .Set<Article>()
                .AddAsync(article, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return article.Id;
        }
    }
}