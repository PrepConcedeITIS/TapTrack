using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetNotificationsOptionsHandler : BaseHandlerWithUserManager<GetNotificationOptionsQuery, bool>
    {
        public GetNotificationsOptionsHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager)
        {
        }

        public override async Task<bool> Handle(GetNotificationOptionsQuery query, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(query.ClaimsPrincipal);

            var notificationOption = await DbContext.Set<TelegramConnection>()
                .Where(x => x.User.Id == user.Id)
                .SingleOrDefaultAsync(cancellationToken);


            return notificationOption?.IsNotificationsEnabled ?? false;
        }
    }
}