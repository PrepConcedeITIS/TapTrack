namespace TapTrackAPI.Core.Features.Profile.GetTelegramInfo
{
    public class TelegramInfo
    {
        public TelegramInfo(bool isConnected, bool isEnabled, string telegramUserName, string userId)
        {
            IsConnected = isConnected;
            IsEnabled = isEnabled;
            TelegramUserName = telegramUserName;
            UserId = userId;
        }
        public bool IsConnected { get; protected set; }
        
        public bool IsEnabled { get; protected set; }
        public string TelegramUserName { get; protected set; }
        
        public string UserId { get; protected set; }
    }
}