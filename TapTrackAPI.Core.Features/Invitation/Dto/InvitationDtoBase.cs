namespace TapTrackAPI.Core.Features.Invitation.Dto
{
    public abstract class InvitationDtoBase
    {
        public string UserName { get; set; } 
        public string Status { get; set; } 
        public string ProjectName { get; set; } 
        public string Role { get; set; }
    }
}