using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using TapTrackAPI.Core.Features.Invitation.Dto;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationResults
{
    public class GetInvitationResultsQuery : IRequest<List<InvitationGridDto>>
    {
        public GetInvitationResultsQuery(Guid projectId)
        {
            ProjectId = projectId;
        }
        [Required]
        public Guid ProjectId { get; set; }
    }
}