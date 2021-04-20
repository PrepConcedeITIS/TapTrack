using System;
using MediatR;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Queries
{
    public class GetIssueQuery : IRequest<IssueDetailsDto>
    {
        public Guid Id { get; set; }
    }
}