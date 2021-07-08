using System.Collections.Generic;

namespace TapTrackAPI.Core.Features.KnowledgeBase.List
{
    public record ProjectWithArticlesDto(string Name, List<ShortArticleDto> Articles);
}