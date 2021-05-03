using System;

namespace TapTrackAPI.Core.Features.Issue.Dtos
{
    public class IssueListItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Project { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
        public string Creator { get; set; }
        public string Assignee { get; set; }
        public string Estimate { get; set; }
        public string Spent { get; set; }
    }
}