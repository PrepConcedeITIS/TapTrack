using System.Collections.Generic;
using MediatR;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Queries
{
    public record GetAllArticlesQuery() : IRequest<List<ProjectWithArticlesDto>>;
}