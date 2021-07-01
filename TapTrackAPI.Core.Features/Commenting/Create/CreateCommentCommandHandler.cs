using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;
using TapTrackAPI.Core.Records;

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
            var teamMember = await DbContext
                .Set<TeamMember>()
                .Where(member => member.ProjectId == request.ProjectId)
                .Include(member => member.User)
                .SingleAsync(member => member.UserId == request.UserId, cancellationToken);
            
            var comment = new Comment(teamMember.Id, request.EntityType, request.EntityId, request.Text);
            DbContext.Entry(comment).State = EntityState.Added;
            await DbContext.SaveChangesAsync(cancellationToken);
            var dto = Mapper.Map<CommentDTO>(comment);
            dto.Author = Mapper.Map<TeamMemberDto>(teamMember);
            return dto;
        }
    }
}