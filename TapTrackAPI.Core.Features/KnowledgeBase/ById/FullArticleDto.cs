using System;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.KnowledgeBase.ById
{
    public record FullArticleDto
    {
        public Guid Id { get; set; }
        public Guid BelongsToId { get; set; }
        public string ProjectTitle { get; set; }
        public string Title { get; set; }
        public TeamMemberDto CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public TeamMemberDto UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Content { get; set; }
    }
}