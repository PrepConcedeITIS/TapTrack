using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;

namespace TapTrackAPI.Core.Features.Profile.Handlers
{
    public class GetNotificationsOptionsHandler : ProfileHandlerWithDbContextBase<GetNotificationOptionsQuery, bool>
    {
        public GetNotificationsOptionsHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<bool> Handle(GetNotificationOptionsQuery query)
        {
            var user = await UserManager.GetUserAsync(query.ClaimsPrincipal);

            var notificationOption = user.UserContacts
                .FirstOrDefault(x => x.ContactType == ContactType.Telegram);

            if (notificationOption == null)
                throw new Exception();

            return notificationOption.NotificationEnabled;
        }
    }
}