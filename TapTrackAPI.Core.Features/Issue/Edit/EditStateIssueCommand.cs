using System;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public record EditStateIssueCommand(string Id, State State, ClaimsPrincipal User) : IRequest<Guid>;
}
