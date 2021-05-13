using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Profile.GetTelegramInfo
{
    [UsedImplicitly]
    public class GetTelegramInfoAsyncQueryHandler : RequestHandlerBase,
        IRequestHandler<GetTelegramInfoQuery, TelegramInfo>
    {
        private readonly UserManager<User> _userManager;

        public GetTelegramInfoAsyncQueryHandler(DbContext context, IMapper mapper, UserManager<User> userManager) :
            base(context, mapper)
        {
            _userManager = userManager;
        }

        public async Task<TelegramInfo> Handle(GetTelegramInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserIdGuid(request.ClaimsPrincipal);
            var existingConnection = await Context.Set<TelegramConnection>()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (existingConnection == default)
                return new TelegramInfo(false, false, null, userId.ToString());

            return new TelegramInfo(true, existingConnection.IsNotificationsEnabled, existingConnection.UserName, userId.ToString());
        }
    }
}