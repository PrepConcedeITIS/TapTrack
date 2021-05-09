using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.TelegramBot.Base;

namespace TapTrackAPI.TelegramBot.Commands.Start
{
    [UsedImplicitly]
    public class BindUserAsyncHandler : RequestHandlerBase, IRequestHandler<BindUserCommand, RequestResponse>
    {
        public BindUserAsyncHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<RequestResponse> Handle(BindUserCommand request, CancellationToken cancellationToken)
        {
            var userIdAvailable = Guid.TryParse(request.UserId, out var userId);
            if (!userIdAvailable)
                return new RequestResponse(false,
                    "Payload from TapTrack service was not passed or was in incorrect format, please try again");

            var possibleConnection = await Context.Set<TelegramConnection>()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.TelegramUserId == request.TelegramUserId || x.UserId == userId,
                    cancellationToken);
            if (possibleConnection != null)
                return new RequestResponse(false,
                    $"Your telegram account already linked to {possibleConnection.User.Email} account");

            var tgConnection =
                new TelegramConnection(request.ChatId, request.TelegramUserId, request.UserName, userId);

            var entityEntry = await Context.AddAsync(tgConnection, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return new RequestResponse(true,
                $"You successfully linked your telegram account to user with {entityEntry.Entity.User.Email} email in TapTrack service");
        }
    }
}