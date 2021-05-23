using System;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public class GetIssueQuery : IRequest<IssueDetailsDto>
    {
        public Guid Id { get; set; }
    }
}