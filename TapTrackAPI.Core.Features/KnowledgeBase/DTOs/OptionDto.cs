using System;

namespace TapTrackAPI.Core.Features.KnowledgeBase.DTOs
{
    public record OptionDto
    {
        public Guid Value { get; init; }
        public string Label { get; init; }
    }
}