using System;

namespace TapTrackAPI.Core.Features.Issue.Dtos
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
        public int EstimationHours { get; set; }
        public int EstimationMinutes { get; set; }
        public string Created { get; set; }
        public int SpentHours { get; set; }
        public int SpentMinutes { get; set; }
        
        public string IdVisible { get; set; }
    }
}