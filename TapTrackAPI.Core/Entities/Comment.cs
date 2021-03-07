using System;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class Comment : EntityBase
    {
        public virtual TeamMember Author { get; protected set; }
        public string Text { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public bool IsDeleted { get; protected set; }
    }
}