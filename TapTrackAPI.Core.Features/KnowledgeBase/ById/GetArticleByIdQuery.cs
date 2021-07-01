using System;
using MediatR;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.KnowledgeBase.ById
{
    public record GetArticleByIdQuery(Guid Id) : IRequest<FullArticleDto>;
}