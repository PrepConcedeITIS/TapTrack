using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Delete
{
    public class IssueDeleteCommand: IRequest
    {
        public IssueDeleteCommand(Guid issueId)
        {
            IssueId = issueId;
        }
        public Guid IssueId { get; init; }
    }
}