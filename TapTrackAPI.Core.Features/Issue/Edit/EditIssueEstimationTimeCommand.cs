using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public record EditIssueEstimationTimeCommand(string Id, string Estimation): IRequest<Guid>;
}