using System;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.Commenting
{
    public record CommentDTO
    {
        public Guid Id { get; init; }
        public UserDto Author { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; init; }
        public DateTime Created { get; init; }
        public DateTime LastUpdated { get; init; }
        public bool IsEditable { get; set; } = true;
        public bool IsDeletable { get; set; } = true;
        public string Mode { get; init; } = "preview";
    }
}