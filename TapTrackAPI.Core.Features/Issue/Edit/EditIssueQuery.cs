using System;
using MediatR;
using TapTrackAPI.Core.Features.Issue.Get;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditIssueQuery: IRequest<EditIssueDto>
    {
        public Guid Id { get; set; }

        public EditIssueQuery(Guid issueId)
        {
            Id = issueId;
        }

        public EditIssueQuery()
        {
            
        }
    }
}