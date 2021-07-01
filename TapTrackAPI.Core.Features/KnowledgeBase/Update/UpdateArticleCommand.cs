using System;
using MediatR;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Update
{
    public record UpdateArticleCommand
        (Guid Id, Guid BelongsToId, string Title, Guid UserId, string Content) : IRequest<Guid>;
}