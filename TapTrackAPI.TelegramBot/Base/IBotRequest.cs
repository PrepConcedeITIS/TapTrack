using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.TelegramBot.Interfaces;

namespace TapTrackAPI.TelegramBot.Base
{
    public interface IBotRequest
    {
        string Command { get; }
        string Description { get; }
        bool InternalCommand { get; }

        Task Execute(IChatService chatService, DbContext dbContext, long chatId, int userId, int messageId,
            string? commandText);
    }
}
