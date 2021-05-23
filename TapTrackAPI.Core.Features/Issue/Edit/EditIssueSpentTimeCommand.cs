using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public record EditIssueSpentTimeCommand(string Id, string Spent): IRequest<Guid>;
}