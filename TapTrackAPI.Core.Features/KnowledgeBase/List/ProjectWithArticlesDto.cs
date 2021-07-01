using System.Collections.Generic;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.KnowledgeBase.List
{
    public record ProjectWithArticlesDto(string Name, List<ShortArticleDto> Articles);
}