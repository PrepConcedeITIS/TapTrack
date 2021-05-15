using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using TapTrackAPI.Core.Features.Project;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class AcceptOrDeclineCommand : IRequest<ProjectDto>
    {
        [Required]
        public Guid InvitationId { get; set; }
        [Required]
        public bool IsAccept { get; set; }
    }
}