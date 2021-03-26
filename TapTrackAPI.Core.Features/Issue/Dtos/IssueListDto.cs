namespace TapTrackAPI.Core.Features.Issue.Dtos
{
    public record IssueListDto(string Title, string Project, string Priority, string State, string Creator,
        string Assignee, string Estimate, string Spent);
}