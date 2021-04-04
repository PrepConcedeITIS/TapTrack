using System;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Dtos
{
    public record ShortProjectDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}