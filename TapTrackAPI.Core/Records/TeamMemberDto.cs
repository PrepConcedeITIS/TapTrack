using System;

namespace TapTrackAPI.Core.Records
{
    public record TeamMemberDto
    {
        public Guid UserId { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
    }
}