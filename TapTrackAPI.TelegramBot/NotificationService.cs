using System.Threading.Tasks;

namespace TapTrackAPI.TelegramBot
{
    public class NotificationService : INotificationService
    {
        private readonly IChatService _chatService;

        public NotificationService(IChatService chatService)
        {
            _chatService = chatService;
        }

        public Task<bool> SendNotificationToUser(long chatId, string message)
        {
            return _chatService.SendMessage(chatId, message);
        }
    }

    public interface INotificationService
    {
        Task<bool> SendNotificationToUser(long chatId, string message);
    }
}