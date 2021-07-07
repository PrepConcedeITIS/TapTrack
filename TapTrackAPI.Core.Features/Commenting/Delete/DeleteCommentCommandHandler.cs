using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Commenting.Delete
{
    [UsedImplicitly]
    public class DeleteCommentCommandHandler : BaseHandler<DeleteCommentCommand, Unit>
    {
        public DeleteCommentCommandHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await DbContext
                .Set<Comment>()
                .FindAsync(new object[] {request.Id}, cancellationToken);
            if (request.IsCommentBeingDeletedPermanently)
                DbContext.Remove(comment);
            else
                comment.MarkAsDeleted();
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}