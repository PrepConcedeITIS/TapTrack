using System;
using System.Collections.Generic;
using MediatR;

namespace TapTrackAPI.Core.Features.Commenting.List
{
    public record GetEntityCommentListQuery
        (string EntityType, Guid ProjectId, Guid EntityId, Guid UserId) : IRequest<List<CommentDTO>>;
}