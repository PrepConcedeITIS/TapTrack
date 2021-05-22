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
    public class RestoreCommentCommandHandler : BaseCommandHandler, IRequestHandler<RestoreCommentCommand, Unit>
    {
        public RestoreCommentCommandHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(RestoreCommentCommand request, CancellationToken cancellationToken)
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