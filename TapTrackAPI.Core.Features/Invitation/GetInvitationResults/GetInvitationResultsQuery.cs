using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationResults
{
    public class GetInvitationResultsQuery : IRequest<List<InvitationDto>>
    {
        [Required]
        public Guid ProjectId { get; set; }
    }
}