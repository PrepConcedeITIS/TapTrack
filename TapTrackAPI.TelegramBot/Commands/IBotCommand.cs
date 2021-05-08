using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.TelegramBot.Commands
{
    public interface IBotCommand
    {
        string Command { get; }
        string Description { get; }
        bool InternalCommand { get; }

        Task Execute(IChatService chatService, DbContext? dbContext, long chatId, int userId, int messageId,
            string? commandText);
    }
}
