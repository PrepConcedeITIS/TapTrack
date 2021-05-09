using System;
using MediatR;
using TapTrackAPI.TelegramBot.Base;

namespace TapTrackAPI.TelegramBot.Commands.Start
{
    public class BindUserCommand : IRequest<RequestResponse>
    {
        public long ChatId { get; protected set; }
        public int TelegramUserId { get; protected set; }
        public string UserName { get; protected set; }
        public string UserId { get; protected set; }

        public BindUserCommand(long chatId, int telegramUserId, string userName, string userId)
        {
            ChatId = chatId;
            TelegramUserId = telegramUserId;
            UserName = userName;
            UserId = userId;
        }
    }
}