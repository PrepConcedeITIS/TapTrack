using System;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class TelegramConnection : HasId<Guid>
    {
        public long ChatId { get; protected set; }
        public int TelegramUserId { get; protected set; }
        public string UserName { get; protected set; }
        public bool IsNotificationsEnabled { get; protected set; }

        public Guid UserId { get; protected set; }
        public virtual User User { get; protected set; }

        protected TelegramConnection()
        {
        }

        public TelegramConnection(long chatId, int telegramUserId, string userName, Guid userId,
            bool isNotificationsEnabled)
        {
            ChatId = chatId;
            TelegramUserId = telegramUserId;
            UserName = userName;
            UserId = userId;
            IsNotificationsEnabled = isNotificationsEnabled;
        }

        public void ChangeNotificationOption()
        {
            IsNotificationsEnabled = !IsNotificationsEnabled;
        }
    }
}