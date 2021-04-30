using System.Collections.Generic;

namespace TapTrackAPI.Core.Features.KnowledgeBase.DTOs
{
    public record ProjectWithArticlesDto(string Name, List<ShortArticleDto> Articles);
}