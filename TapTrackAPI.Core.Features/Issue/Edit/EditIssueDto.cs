using System;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditIssueDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
    }
}