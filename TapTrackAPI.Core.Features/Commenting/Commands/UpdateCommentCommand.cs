using System;
using MediatR;
using TapTrackAPI.Core.Features.Commenting.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.Commands
{
    public record UpdateCommentCommand : IRequest<EditedCommentDTO>
    {
        public Guid Id { get; init; }
        public string Text { get; init; }
        public Guid UserId { get; init; }
    }
}