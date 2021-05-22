namespace TapTrackAPI.Core.Features.Issue.Services
{
    public record IssueDetailsDropdownsSchema(string[] IssueType, string[] Priority, string[] Assignee, string[] State);
}