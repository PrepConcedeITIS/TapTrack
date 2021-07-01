using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Commenting.Create
{
    public record CreateCommentCommand
        (string EntityType, Guid EntityId, Guid ProjectId, string Text, Guid UserId) : IRequest<CommentDTO>;
}