using System;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Entities
{
    public class UserContact : EntityBase
    {
        public UserContact(Guid userId, string contactInfo, bool notificationEnabled, Guid contactTypeId)
        {
            UserId = userId;
            ContactInfo = contactInfo;
            NotificationEnabled = notificationEnabled;
            ContactTypeId = contactTypeId;
        }

        #region Properties

        public Guid UserId { get; protected set; }
        public string ContactInfo { get; protected set; }
        public bool NotificationEnabled { get; protected set; }
        public Guid ContactTypeId { get; protected set; }

        public virtual User User { get; set; }
        public virtual ContactType ContactType { get; set; }

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