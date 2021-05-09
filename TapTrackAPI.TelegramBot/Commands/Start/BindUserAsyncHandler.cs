using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.TelegramBot.Base;

namespace TapTrackAPI.TelegramBot.Commands.Start
{
    [UsedImplicitly]
    public class BindUserAsyncHandler : IRequestHandler<BindUserCommand, RequestResponse>
    {
        public async Task<RequestResponse> Handle(BindUserCommand request, CancellationToken cancellationToken)
        {
            var dbContext = request.DbContext;
            var userIdAvailable = Guid.TryParse(request.UserId, out var userId);
            if (!userIdAvailable)
                return new RequestResponse(false,
                    "Payload from TapTrack service was not passed or was in incorrect format, please try again");

            var possibleConnection = await dbContext.Set<TelegramConnection>()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.TelegramUserId == request.TelegramUserId || x.UserId == userId,
                    cancellationToken);
            if (possibleConnection != null)
                return new RequestResponse(false,
                    $"Your telegram account already linked to {possibleConnection.User.Email} account");

            var tgConnection =
                new TelegramConnection(request.ChatId, request.TelegramUserId, request.UserName, userId, true);

            var entityEntry = await dbContext.AddAsync(tgConnection, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var email = await dbContext.Set<TelegramConnection>()
                .Where(x => x.Id == entityEntry.Entity.Id)
                .Select(x => x.User.Email)
                .FirstOrDefaultAsync(cancellationToken);
            return new RequestResponse(true,
                $"You successfully linked your telegram account to user with {email} email in TapTrack service");
        }
    }
}