using System.Collections.Generic;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public record GetIssueListQuery : IRequest<List<IssueListItemDto>>;
}