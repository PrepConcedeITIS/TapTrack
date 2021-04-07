using System;

namespace TapTrackAPI.Core.Features.Issue.Dtos
{
    public record IssueDetailsDto(string Title, string Description, string Creator, string Assignee,
        string IssueType, string Priority, string Project, string State, int EstimationHours,int EstimationMinutes, string Created,
        int SpentHours,int SpentMinutes);
}