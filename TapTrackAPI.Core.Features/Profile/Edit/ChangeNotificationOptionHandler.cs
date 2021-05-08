using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class
        ChangeNotificationOptionHandler : ProfileHandlerWithDbContextBase<ChangeNotificationOptionsCommand, bool>
    {
        public ChangeNotificationOptionHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<bool> Handle(ChangeNotificationOptionsCommand command,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);

            var userContact = DbContext.Set<UserContact>()
                .Where(x => x.User.Id == user.Id)
                .Include(x => x.ContactType)
                .FirstOrDefault(x => x.ContactType.Name == ContactTypeConstants.TelegramName);


            if (userContact == null) 
                return command.Option;
            
            userContact.ChangeNotificationOption(command.Option);
            DbContext.Set<UserContact>().Update(userContact);
            await DbContext.SaveChangesAsync(cancellationToken);

            return command.Option;
        }
    }
}