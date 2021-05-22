using System;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public class IssueDetailsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public string Assignee { get; set; }
        public string IssueType { get; set; }
        public string Priority { get; set; }
        public string Project { get; set; }
        public Guid ProjectId { get; set; }
        public string State { get; set; }
        public string Created { get; set; }
        public string IdVisible { get; set; }
        public string Spent { get; set; }
        public string Estimate { get; set; }
    }
}