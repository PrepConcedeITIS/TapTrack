using System;

namespace TapTrackAPI.Core.Records
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
    }
}