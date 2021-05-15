using System;

namespace TapTrackAPI.Core.Features.KnowledgeBase.DTOs
{
    public record TeamMemberDto
    {
        public Guid UserId { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
    }
}