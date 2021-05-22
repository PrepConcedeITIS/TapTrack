using System;
using MediatR;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditPriorityIssueCommand : IRequest<Guid>
    {
        public string Id { get; set; }

        public Priority Priority { get; set; }
    }
}
