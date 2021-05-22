using System;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Features.Issue.Base;
using TapTrackAPI.Core.Interfaces;


namespace TapTrackAPI.Core.Features.Issue.Delete
{
    public class IssueDeleteCommand : IRequest, IHasClaims, IHasIssueId
    {
        public IssueDeleteCommand(ClaimsPrincipal claimsPrincipal, Guid issueId)
        {
            ClaimsPrincipal = claimsPrincipal;
            IssueId = issueId;
        }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public Guid IssueId { get; init; }
    }
}