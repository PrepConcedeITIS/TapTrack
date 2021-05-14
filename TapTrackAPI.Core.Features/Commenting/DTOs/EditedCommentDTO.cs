using System;

namespace TapTrackAPI.Core.Features.Commenting.DTOs
{
    public class EditedCommentDTO
    {
        public string Text { get; init; }
        public DateTime LastUpdated { get; init; }
    }
}