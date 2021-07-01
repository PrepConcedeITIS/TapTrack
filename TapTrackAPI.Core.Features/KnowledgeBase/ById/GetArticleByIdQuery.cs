using System;
using MediatR;

namespace TapTrackAPI.Core.Features.KnowledgeBase.ById
{
    public record GetArticleByIdQuery(Guid Id) : IRequest<FullArticleDto>;
}