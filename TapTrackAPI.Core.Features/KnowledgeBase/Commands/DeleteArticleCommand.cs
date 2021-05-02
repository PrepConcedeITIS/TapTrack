using System;
using MediatR;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Commands
{
    public record DeleteArticleCommand(Guid Id, Guid BelongsToId, Guid UserId) : IRequest<Unit>;
}