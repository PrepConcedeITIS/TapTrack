using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class InvitationDto
    {
        public string UserName { get; set; } 
        public string InvitationState { get; set; } 
        public string ProjectName { get; set; } 
        public string InvitationRole { get; set; }
    }
}