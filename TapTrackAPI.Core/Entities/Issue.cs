using System;
using System.Collections.Generic;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Entities
{
    public class Issue : EntityBase
    {
        protected Issue()
        {
            
        }
        
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public virtual TeamMember Creator { get; protected set; }
        public virtual TeamMember Assignee { get; protected set; }
        public TimeSpan Estimation { get; protected set; }
        public TimeSpan Spent { get; protected set; }
        public State State { get; protected set; }
        public IssueType IssueType { get; protected set; }
        public Priority Priority { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime LastUpdated { get; protected set; }

        public virtual ICollection<Comment> Comment { get; protected set; }
    }
}