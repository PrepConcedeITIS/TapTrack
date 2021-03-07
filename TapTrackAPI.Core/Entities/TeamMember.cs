using System;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Entities
{
    public class TeamMember : HasId<long>
    {
        public Guid UserId { get; protected set; }
        public virtual User User { get; protected set; }

        public Guid ProjectId { get; protected set; }
        public virtual Project Project { get; protected set; }

        public string Role { get; protected set; }

        protected TeamMember(Guid userId, Guid projectId, Role role)
        {
            UserId = userId;
            ProjectId = projectId;
            Role = role.ToString();
        }

        protected TeamMember()
        {
        }
    }
}