using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.TelegramBot.Commands.Start;

namespace TapTrackAPI.TelegramBot.Commands
{
    [UsedImplicitly]
    public class StartRequest : IBotRequest
    {
        private readonly IMediator _mediator;

        public StartRequest(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string Command => "start";
        public string Description => "Start command";
        public bool InternalCommand => false;

        public async Task Execute(IChatService chatService, long chatId, int userId, int messageId, string? commandText)
        {
            if (commandText == null)
                await chatService.SendMessage(chatId, "Payload from TapTrack service was not passed, please try again");

            var a = await _mediator.Send(new BindUserCommand(chatId, userId,
                await chatService.GetChatMemberName(chatId, userId),
                commandText!));
            var tgConnection = new TelegramConnection(chatId, userId,
                await chatService.GetChatMemberName(chatId, userId),
                Guid.Parse(commandText!));
        }
    }
}