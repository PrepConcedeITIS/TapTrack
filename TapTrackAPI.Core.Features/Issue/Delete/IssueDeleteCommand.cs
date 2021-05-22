using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Delete
{
    public record IssueDeleteCommand(Guid IssueId) : IRequest;
}