using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Queries
{
    public class GetIssuesByProjectIdQuery : IRequest<List<IssueOnBoardDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
