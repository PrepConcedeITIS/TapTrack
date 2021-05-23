using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public record GetIssueListQuery : IRequest<List<IssueListItemDto>>, IHasClaims
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}