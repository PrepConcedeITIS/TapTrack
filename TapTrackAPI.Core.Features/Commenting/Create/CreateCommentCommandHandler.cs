using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Commenting.Create
{
    [UsedImplicitly]
    public class CreateCommentCommandHandler : BaseHandler<CreateCommentCommand, CommentDTO>
    {
        public CreateCommentCommandHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<CommentDTO> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await DbContext.Set<User>()
                .Where(u => u.Id == request.UserId)
                .Select(u => new
                {
                    User = u,
                    TeamMemberId = u.TeamMembers
                        .FirstOrDefault(tm => tm.ProjectId == request.ProjectId).Id
                })
                .FirstOrDefaultAsync(cancellationToken);

            var comment = new Comment(user.TeamMemberId, request.EntityType, request.EntityId, request.Text);
            var entityEntry = await DbContext.Set<Comment>().AddAsync(comment, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);

            var commentDto = await DbContext.Set<Comment>()
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == entityEntry.Entity.Id, cancellationToken);

            return commentDto;
        }
    }
}