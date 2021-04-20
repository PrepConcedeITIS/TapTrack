using System;
using System.Security.Claims;
using MediatR;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Commands
{
    public record CreateArticleCommand
        (Guid BelongsToId, string Title, string Content, ClaimsPrincipal AppUser) : IRequest<Guid>;
}