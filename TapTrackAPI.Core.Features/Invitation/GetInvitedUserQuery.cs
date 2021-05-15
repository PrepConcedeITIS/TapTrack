using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class GetInvitedUserQuery : IRequest<List<InvitationDto>>
    {
        [Required]
        public Guid ProjectId { get; set; }
    }
}