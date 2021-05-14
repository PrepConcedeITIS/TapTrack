using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Features.Commenting.Commands;
using TapTrackAPI.Core.Features.Commenting.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.Handlers
{
    public class EditCommentCommandHandler : BaseCommandHandler, IRequestHandler<UpdateCommentCommand, EditedCommentDTO>
    {
        public EditCommentCommandHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<EditedCommentDTO> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await DbContext
                .Set<Comment>()
                .FindAsync(new object[] {request.Id}, cancellationToken);
            comment.UpdateText(request.Text);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<EditedCommentDTO>(comment);
        }
    }
}