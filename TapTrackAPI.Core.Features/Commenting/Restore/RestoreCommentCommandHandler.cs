using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Commenting.Restore
{
    [UsedImplicitly]
    public class RestoreCommentCommandHandler : BaseHandler<RestoreCommentCommand, Unit>
    {
        public RestoreCommentCommandHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Unit> Handle(RestoreCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await DbContext
                .Set<Comment>()
                .FindAsync(new object[] {request.Id}, cancellationToken);
            comment.UnmarkAsDeleted();
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}