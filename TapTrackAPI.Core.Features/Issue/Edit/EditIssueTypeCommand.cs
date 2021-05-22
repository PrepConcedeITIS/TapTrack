using System;
using MediatR;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditIssueTypeCommand : IRequest<Guid>
    {
        public string Id { get; set; }

        public IssueType IssueType { get; set; }   
    }
}