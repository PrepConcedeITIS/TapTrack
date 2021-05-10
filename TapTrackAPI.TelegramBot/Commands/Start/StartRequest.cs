using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.TelegramBot.Base;

namespace TapTrackAPI.TelegramBot.Commands.Start
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

        public async Task Execute(IChatService chatService, DbContext dbContext, long chatId, int userId, int messageId,
            string? commandText)
        {
            var requestResponse = await _mediator.Send(new BindUserCommand(chatId, userId,
                await chatService.GetChatMemberName(chatId, userId),
                commandText!, dbContext));

            await chatService.SendMessage(chatId, requestResponse.Message);
        }
    }
}