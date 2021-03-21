using System;
using System.Collections.Generic;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class Project : EntityBase
    {
        protected Project()
        {
        }

        public Project(string name, string idVisible, string description, string logoUrl, Guid creatorId) :
            base(idVisible)
        {
            Name = name;
            Description = description;
            LogoUrl = logoUrl;
            CreatorId = creatorId;
        }
        public Project(string name, string idVisible, string description, Guid creatorId) :
            base(idVisible)
        {
            Name = name;
            Description = description;
            CreatorId = creatorId;
        }

        #region Properties

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string LogoUrl { get; protected set; }

        public Guid CreatorId { get; protected set; }
        public virtual User Creator { get; protected set; }

        public virtual ICollection<Issue> Issues { get; protected set; }

        public virtual ICollection<TeamMember> Team { get; protected set; }

        #endregion

        public void UpdateLogoUrl(string logoUrl)
        {
            LogoUrl = logoUrl;
        }
    }
}