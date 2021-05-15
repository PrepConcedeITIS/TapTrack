using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Entities
{
    public class Invitation : EntityBase
    {
        public User User { get; protected set; }

        public Project Project { get; protected set; }

        public InvitationState InvitationState { get; protected set; }

        public Role InvitationRole { get; protected set; }

        public Invitation(User user, Project project, InvitationState invitationState, Role invitationRole)
        {
            User = user;
            Project = project;
            InvitationState = invitationState;
            InvitationRole = invitationRole;
        }

        protected Invitation()
        {
            
        }

        public void SetAcceptState(bool isAccept)
        {
            if (isAccept)
            {
                InvitationState = InvitationState.Accept;
            }
            else
            {
                InvitationState = InvitationState.Decline;
            }
        }
    }
}