using System;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Entities
{
    public class Invitation : EntityBase
    {
        public Guid UserId { get; protected set; }
        public virtual User User { get; protected set; }
        public Guid ProjectId { get; protected set; }

        public virtual Project Project { get; protected set; }

        public InvitationState InvitationState { get; protected set; }

        public Role InvitationRole { get; protected set; }

        public Invitation(Guid userId, Guid projectId, InvitationState invitationState, Role invitationRole)
        {
            UserId = userId;
            ProjectId = projectId;
            InvitationState = invitationState;
            InvitationRole = invitationRole;
        }

        protected Invitation()
        {
        }

        public void SetAcceptState(bool isAccept)
        {
            InvitationState = isAccept ? InvitationState.Accept : InvitationState.Decline;
        }
    }
}