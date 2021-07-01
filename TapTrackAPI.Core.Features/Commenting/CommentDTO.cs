using System;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.Commenting
{
    public record CommentDTO
    {
        public Guid Id { get; init; }
        public TeamMemberDto Author { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; init; }
        public DateTime Created { get; init; }
        public DateTime LastUpdated { get; init; }
        public bool IsEditable { get; set; } = true;
        public bool IsDeletable { get; set; } = true;
        public string Mode { get; init; } = "preview";
    }
}