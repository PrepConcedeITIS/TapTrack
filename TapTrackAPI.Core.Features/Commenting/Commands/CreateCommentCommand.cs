using System;
using MediatR;
using TapTrackAPI.Core.Features.Commenting.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.Commands
{
    public record CreateCommentCommand
        (string EntityType, Guid EntityId, Guid ProjectId, string Text, Guid UserId) : IRequest<CommentDTO>;
}