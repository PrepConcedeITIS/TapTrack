using System;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class UserContact : EntityBase
    {
        public UserContact(Guid userId, string contactInfo, Guid contactTypeId)
        {
            UserId = userId;
            ContactInfo = contactInfo;
            ContactTypeId = contactTypeId;
        }

        #region Properties

        public string ContactInfo { get; protected set; }

        public Guid UserId { get; protected set; }
        public virtual User User { get; set; }

        public Guid ContactTypeId { get; protected set; }
        public virtual ContactType ContactType { get; set; }

        #endregion

        public void UpdateContactInfo(string info)
        {
            ContactInfo = info;
        }
    }
}