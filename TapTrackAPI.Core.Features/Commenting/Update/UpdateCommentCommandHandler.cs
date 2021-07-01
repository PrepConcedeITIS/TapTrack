using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Commenting.Update
{
    [UsedImplicitly]
    public class UpdateCommentCommandHandler : BaseHandler<UpdateCommentCommand, EditedCommentDTO>
    {
        public UpdateCommentCommandHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<EditedCommentDTO> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
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