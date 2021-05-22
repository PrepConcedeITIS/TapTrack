using System;
using System.Collections.Generic;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Get
{
    public class GetIssuesByProjectIdQuery : IRequest<List<IssueOnBoardDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
