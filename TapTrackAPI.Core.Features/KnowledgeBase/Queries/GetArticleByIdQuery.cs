using System;
using MediatR;
using TapTrackAPI.Core.Features.KnowledgeBase.Dtos;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Queries
{
    public class GetArticleByIdQuery : IRequest<FullArticleDto>
    {
        public Guid Id { get; set; }
    }
}