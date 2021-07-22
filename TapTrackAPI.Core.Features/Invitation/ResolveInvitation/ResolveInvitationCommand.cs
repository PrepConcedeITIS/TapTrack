using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Features.Project;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Invitation.ResolveInvitation
{
    public record ResolveInvitationCommand : IRequest<ProjectDto>, IHasClaims
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public bool IsAccept { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}