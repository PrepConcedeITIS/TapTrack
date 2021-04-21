using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetNotificationsOptionsHandler : ProfileHandlerWithDbContextBase<GetNotificationOptionsQuery, bool>
    {
        public GetNotificationsOptionsHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<bool> Handle(GetNotificationOptionsQuery query, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(query.ClaimsPrincipal);

            var notificationOption = DbContext.Set<UserContact>()
                .Where(x => x.User.Id == user.Id)
                .Include( x => x.ContactType)
                .FirstOrDefault(x => x.ContactType.Name == ContactTypeConstants.TelegramName);
            

            return notificationOption.NotificationEnabled;
        }
    }
}