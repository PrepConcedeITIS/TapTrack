using System;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Entities
{
    public class UserContact : EntityBase
    {
        public UserContact(Guid userId, string contactInfo, bool notificationEnabled, ContactType contactType)
        {
            UserId = userId;
            ContactInfo = contactInfo;
            NotificationEnabled = notificationEnabled;
            ContactType = contactType;
        }

        #region Properties

        public Guid UserId { get; protected set; }
        public string ContactInfo { get; protected set; }
        public bool NotificationEnabled { get; protected set; }
        public ContactType ContactType { get; protected set; }

        public virtual User User { get; set; }

        #endregion

        public void UpdateContactInfo(string info)
        {
            ContactInfo = info;
        }

        public void ChangeNotificationOption(bool option)
        {
            NotificationEnabled = option;
        }
    }
}