using System;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public class EditIssueDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
    }
}