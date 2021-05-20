using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Commenting.Commands
{
    public record DeleteCommentCommand : IRequest<Unit>
    {
        public Guid Id { get; init; }
        public bool IsCommentBeingDeletedPermanently { get; init; }
        public Guid ProjectId { get; init; }
        public Guid UserId { get; init; }
    }
}