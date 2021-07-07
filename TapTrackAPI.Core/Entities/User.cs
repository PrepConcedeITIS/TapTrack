using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TapTrackAPI.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        #region Properties

        public string ProfileImageUrl { get; protected set; }

        public virtual ICollection<UserContact> UserContacts { get; protected set; }

        public virtual ICollection<TeamMember> TeamMembers { get; protected set; }
        
        #endregion


        protected User()
        {
        }

        public User(string email)
        {
            Email = email;
            UserName = email.Split('@')[0];
        }

        public void UpdateProfileImage(string imageUrl)
        {
            ProfileImageUrl = imageUrl;
        }
    }
}