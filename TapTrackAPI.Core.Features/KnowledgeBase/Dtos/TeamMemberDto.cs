namespace TapTrackAPI.Core.Features.KnowledgeBase.Dtos
{
    public record TeamMemberDto
    {
        public string Username { get; init; }
        public string Email { get; init; }
    }
}