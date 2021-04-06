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
    public class ChangeNotificationOptionHandler : ProfileHandlerWithDbContextBase<ChangeNotificationOptionsCommand, bool>
    {
        public ChangeNotificationOptionHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<bool> Handle(ChangeNotificationOptionsCommand command)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);

            var userContact = user.UserContacts
                .FirstOrDefault(x => x.ContactType == ContactType.Telegram);


            if (userContact != null)
            {
                userContact.ChangeNotificationOption(command.Option);
                DbContext.Set<UserContact>().Update(userContact);
                await DbContext.SaveChangesAsync();
            }

            return command.Option;
        }
    }
}