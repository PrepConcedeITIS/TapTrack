using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Project.Base;

namespace TapTrackAPI.Core.Features.Invitation.InviteUser
{
    public class InviteUserCommand:IRequest<InvitationDto>, IHasProjectId
    {
        [Required]
        public Guid ProjectId { get; init; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public string Email { get; set; }
    }
}