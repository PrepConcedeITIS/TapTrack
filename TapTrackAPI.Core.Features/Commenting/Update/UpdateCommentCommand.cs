using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Commenting.Update
{
    public record UpdateCommentCommand : IRequest<EditedCommentDTO>
    {
        public Guid Id { get; init; }
        public string Text { get; init; }
        public Guid UserId { get; init; }
    }
}