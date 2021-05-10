using System;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.DTOs
{
    public record CommentDTO
    {
        public TeamMemberDto Author { get; set; }
        public string Text { get; init; }
        public DateTime Created { get; init; }
        public DateTime LastUpdated { get; init; }
    }
}