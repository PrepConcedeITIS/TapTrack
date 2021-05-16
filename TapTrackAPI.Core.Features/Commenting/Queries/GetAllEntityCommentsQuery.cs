using System;
using System.Collections.Generic;
using MediatR;
using TapTrackAPI.Core.Features.Commenting.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.Queries
{
    public record GetAllEntityCommentsQuery(Guid EntityId, string EntityType, Guid UserId) : IRequest<List<CommentDTO>>;
}