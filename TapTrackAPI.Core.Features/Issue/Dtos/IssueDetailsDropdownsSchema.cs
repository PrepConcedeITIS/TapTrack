namespace TapTrackAPI.Core.Features.Issue.Dtos
{
    public record IssueDetailsDropdownsSchema(string[] IssueType, string[] Priority, string[] Assignee, string[] State);
}