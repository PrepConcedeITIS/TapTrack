namespace TapTrackAPI.Core.Features.KnowledgeBase.DTOs
{
    public record TeamMemberDto
    {
        public string Username { get; init; }
        public string Email { get; init; }
    }
}