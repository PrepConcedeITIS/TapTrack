using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public record EditIssueQuery(Guid Id) : IRequest<EditIssueDto>;
}