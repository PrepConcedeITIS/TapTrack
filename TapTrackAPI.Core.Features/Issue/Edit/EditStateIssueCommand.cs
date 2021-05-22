using System;
using MediatR;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditStateIssueCommand : IRequest<Guid>
    {
        public string Id { get; set; }

        public State State { get; set; }
    }
}
