using System;

namespace TapTrackAPI.Core.Base
{
    public abstract class EntityBase : HasId<Guid>
    {
        public string IdVisible { get; protected set; }

        public EntityBase(string idVisible)
        {
            IdVisible = idVisible;
        }

        protected EntityBase()
        {
        }
    }
}