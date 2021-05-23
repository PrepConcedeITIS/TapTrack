using System;
using System.Security.Claims;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public record EditAssigneeIssueCommand(string Id, string Assignee, ClaimsPrincipal User) : IRequest<Guid>;
}