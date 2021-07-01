using System;

namespace TapTrackAPI.Core.Records
{
    public record OptionDto
    {
        public Guid Value { get; init; }
        public string Label { get; init; }
    }
}