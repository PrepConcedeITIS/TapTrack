using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Queries
{
    public class EditPriorityIssueCommand : IRequest<Guid>
    {
        public string Id { get; set; }

        public Priority Priority { get; set; }
    }
}
