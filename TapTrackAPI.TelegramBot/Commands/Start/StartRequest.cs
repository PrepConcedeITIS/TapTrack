using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.TelegramBot.Base;
using TapTrackAPI.TelegramBot.Interfaces;

namespace TapTrackAPI.TelegramBot.Commands.Start
{
    [UsedImplicitly]
    public class StartRequest : IBotRequest
    {
        private readonly IMediator _mediator;
        private readonly bool _tgEnabled; 

        public StartRequest(IMediator mediator, IHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
             bool.TryParse(Environment.GetEnvironmentVariable(ConfigurationConstants.TelegramNotificationsEnabled), out _tgEnabled); 
        }

        public string Command => "start";
        public string Description => "Start command";
        public bool InternalCommand => false;

        public async Task Execute(IChatService chatService, DbContext dbContext, long chatId, int userId, int messageId,
            string? commandText)
        {
            if (!_tgEnabled)
                return;
            var requestResponse = await _mediator.Send(new BindUserCommand(chatId, userId,
                await chatService.GetChatMemberName(chatId, userId),
                commandText!, dbContext));

            await chatService.SendMessage(chatId, requestResponse.Message);
        }
    }
}