using System;
using MediatR;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Delete
{
    public record DeleteArticleCommand(Guid Id, Guid BelongsToId, Guid UserId) : IRequest<Unit>;
}