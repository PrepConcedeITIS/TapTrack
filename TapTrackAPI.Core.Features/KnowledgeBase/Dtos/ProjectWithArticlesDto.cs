using System.Collections.Generic;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Dtos
{
    public record ProjectWithArticlesDto(string Name, List<ShortArticleDto> Articles);
}