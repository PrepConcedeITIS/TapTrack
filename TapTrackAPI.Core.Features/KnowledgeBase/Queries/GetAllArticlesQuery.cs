using System.Collections.Generic;
using MediatR;
using TapTrackAPI.Core.Features.KnowledgeBase.Dtos;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Queries
{
    public class GetAllArticlesQuery : IRequest<List<ProjectWithArticlesDto>>
    {
    }
}