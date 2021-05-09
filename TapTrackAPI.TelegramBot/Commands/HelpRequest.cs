using System;
using System.Threading.Tasks;

namespace TapTrackAPI.TelegramBot.Commands
{
    public class HelpRequest : IBotRequest
    {
        private readonly IServiceProvider _serviceProvider;

        public string Command => "help";

        public string Description => "Get information about the functionality available for this bot";

        public bool InternalCommand => false;

        public HelpRequest(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IChatService chatService, long chatId, int userId, int messageId, string? commandText)
        {
            await chatService.SendMessage(chatId, "TODO: Create a todo command");
        }
    }
}
