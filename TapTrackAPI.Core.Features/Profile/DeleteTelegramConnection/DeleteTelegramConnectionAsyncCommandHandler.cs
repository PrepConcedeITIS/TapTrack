using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Profile.DeleteTelegramConnection
{
    [UsedImplicitly]
    public class DeleteTelegramConnectionAsyncCommandHandler
        : BaseHandlerWithUserManager<DeleteTelegramConnectionCommand, Unit?>
    {
        public DeleteTelegramConnectionAsyncCommandHandler(DbContext context, IMapper mapper,
            UserManager<User> userManager) : base(context, mapper, userManager)
        {
        }

        public override async Task<Unit?> Handle(DeleteTelegramConnectionCommand request,
            CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.ClaimsPrincipal);

            var tgConnection = await DbContext.Set<TelegramConnection>()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            if (tgConnection == default)
                return null;

            DbContext.Set<TelegramConnection>().Remove(tgConnection);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}