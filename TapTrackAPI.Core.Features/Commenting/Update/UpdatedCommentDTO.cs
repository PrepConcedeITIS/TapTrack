using System;

namespace TapTrackAPI.Core.Features.Commenting.Update
{
    public class EditedCommentDTO
    {
        public string Text { get; init; }
        public DateTime LastUpdated { get; init; }
    }
}