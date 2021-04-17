using System.Collections.Generic;
using MediatR;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Queries
{
    public record GetIssueListQuery : IRequest<List<IssueListItemDto>>;
}