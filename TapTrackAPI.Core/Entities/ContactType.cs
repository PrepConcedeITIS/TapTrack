using System;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class ContactType : EntityBase
    {
        public ContactType(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; protected set; }
    }
}