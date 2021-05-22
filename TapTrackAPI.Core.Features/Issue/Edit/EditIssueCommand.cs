using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Edit
{ 
    public record EditIssueCommand(Guid Id, string Title, string Description, Guid ProjectId) : IRequest<Guid>;
}