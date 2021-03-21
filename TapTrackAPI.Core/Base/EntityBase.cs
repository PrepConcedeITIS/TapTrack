using System;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Base
{
    [Index(nameof(IdVisible), IsUnique = true)]
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