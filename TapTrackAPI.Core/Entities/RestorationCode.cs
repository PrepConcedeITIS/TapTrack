using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class RestorationCode : EntityBase
    {
        public string Email { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime ExpirationDate { get; protected set; }
        public int Code { get; protected set; }

        protected RestorationCode()
        {

        }

        public RestorationCode (string mail, DateTime creationTime, DateTime expirationDate, int code)
        {
            Email = mail;
            CreationDate = creationTime;
            ExpirationDate = expirationDate;
            Code = code;
        }

        public void CodeIsUsed()
        {
            this.ExpirationDate = DateTime.Now;
        }
    }
}