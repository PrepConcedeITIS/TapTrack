using System;

namespace TapTrackAPI.Core.Features.Issue.Base
{
    public interface IHasIssueId
    {
        public Guid IssueId { get; init; }

    }
}