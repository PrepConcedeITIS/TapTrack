using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Features.Commenting.Commands;
using TapTrackAPI.Core.Features.Commenting.DTOs;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.Handlers
{
    public class CreateCommentCommandHandler : BaseCommandHandler, IRequestHandler<CreateCommentCommand, CommentDTO>
    {
        public CreateCommentCommandHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CommentDTO> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
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