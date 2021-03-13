using System;
using Microsoft.AspNetCore.Identity;

namespace TapTrackAPI.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string ProfileImageUrl { get; set; }
        
        protected User()
        {
        }

        public User(string email)
        {
            Email = email;
            UserName = email.Split('@')[0];
        }
    }
}