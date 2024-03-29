﻿using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Commenting.Restore
{
    public record RestoreCommentCommand : IRequest<Unit>
    {
        public Guid Id { get; init; }
        public Guid ProjectId { get; init; }
        public Guid UserId { get; init; }
    }
}