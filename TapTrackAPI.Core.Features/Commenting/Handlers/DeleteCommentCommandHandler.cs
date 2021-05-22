using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Features.Commenting.Commands;

namespace TapTrackAPI.Core.Features.Commenting.Handlers
{
    public class DeleteCommentCommandHandler : BaseCommandHandler, IRequestHandler<DeleteCommentCommand, Unit>
    {
        public DeleteCommentCommandHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await DbContext
                .Set<Comment>()
                .FindAsync(new object[] {request.Id}, cancellationToken);
            if (request.IsCommentBeingDeletedPermanently)
                DbContext.Entry(comment).State = EntityState.Deleted;
            else
                comment.MarkAsDeleted();
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}