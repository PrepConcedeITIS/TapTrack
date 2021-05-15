using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Issue.Queries
{
    public class EditStateIssueCommand : IRequest<Guid>
    {
        public string Id { get; set; }

        public State State { get; set; }
    }
}
