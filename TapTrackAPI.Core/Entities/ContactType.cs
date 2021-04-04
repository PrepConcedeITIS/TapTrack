using System;
using System.Collections.Generic;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class ContactType : EntityBase
    {
        public string Name { get; protected set; }

        public virtual ICollection<UserContact> UserContacts { get; protected set; }
        
        public ContactType(string name, Guid id)
        {
            Id = id;
            Name = name;
        }

        public ContactType(string name)
        {
            Name = name;
        }
    }
}