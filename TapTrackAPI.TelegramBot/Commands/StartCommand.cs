using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.TelegramBot.Commands
{
    [UsedImplicitly]
    public class StartCommand : IBotCommand
    {
        public string Command => "start";
        public string Description => "Start command";
        public bool InternalCommand => false;

        public async Task Execute(IChatService chatService, DbContext? dbContext, long chatId, int userId, int messageId, string? commandText)
        {
            if (commandText == null)
                await chatService.SendMessage(chatId, "Payload from TapTrack service was not passed, please try again");

            var tgConnection = new TelegramConnection(chatId, userId,
                await chatService.GetChatMemberName(chatId, userId),
                Guid.Parse(commandText!));
        }
    }
}