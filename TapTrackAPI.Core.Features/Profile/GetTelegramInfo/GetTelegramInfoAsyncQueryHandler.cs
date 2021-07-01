using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Profile.GetTelegramInfo
{
    [UsedImplicitly]
    public class GetTelegramInfoAsyncQueryHandler : BaseHandlerWithUserManager<GetTelegramInfoQuery, TelegramInfo>
    {
        public GetTelegramInfoAsyncQueryHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager)
        {
        }

        public override async Task<TelegramInfo> Handle(GetTelegramInfoQuery request,
            CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.ClaimsPrincipal);
            var existingConnection = await DbContext.Set<TelegramConnection>()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (existingConnection == default)
                return new TelegramInfo(false, false, null, userId.ToString());

            return new TelegramInfo(true, existingConnection.IsNotificationsEnabled, existingConnection.UserName,
                userId.ToString());
        }
    }
}