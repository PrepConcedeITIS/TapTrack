using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditAssigneeIssueCommand : IRequest<Guid>
    {
        public string Id { get; set; }

        public string Assignee { get; set; }
    }
}