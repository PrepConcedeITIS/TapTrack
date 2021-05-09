using System.Threading.Tasks;

namespace TapTrackAPI.TelegramBot.Commands
{
    public interface IBotRequest
    {
        string Command { get; }
        string Description { get; }
        bool InternalCommand { get; }

        Task Execute(IChatService chatService, long chatId, int userId, int messageId,
            string? commandText);
    }
}
