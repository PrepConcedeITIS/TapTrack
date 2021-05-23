using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationResults
{
    public class GetInvitationResultsQuery : IRequest<List<InvitationDto>>
    {
        public GetInvitationResultsQuery(Guid projectId)
        {
            ProjectId = projectId;
        }
        [Required]
        public Guid ProjectId { get; set; }
    }
}