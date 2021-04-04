using System;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class UserContact : EntityBase
    {
        public UserContact(Guid userId, Guid contactTypeId, string contactInfo, bool notificationEnabled)
        {
            UserId = userId;
            ContactTypeId = contactTypeId;
            ContactInfo = contactInfo;
            NotificationEnabled = notificationEnabled;
        }

        #region Properties

        public Guid UserId { get; protected set; }
        public Guid ContactTypeId { get; protected set; }
        public string ContactInfo { get; protected set; }
        public bool NotificationEnabled { get; protected set; }

        public virtual User User { get; set; }
        public virtual ContactType ContactType { get; protected set; }

        #endregion

        public void UpdateContactInfo(string info)
        {
            ContactInfo = info;
        }
    }
}