using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class
        UpdateContactsInfoHandler : ProfileHandlerWithDbContextBase<UpdateContactInfoCommand,
            List<ContactInformationListItemDto>>
    {
        public UpdateContactsInfoHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<List<ContactInformationListItemDto>> Handle(UpdateContactInfoCommand command, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);


            var userContactsList = await DbContext.Set<UserContact>()
                .Where(x => x.UserId == user.Id)
                .Include(x => x.ContactType)
                .ToListAsync(cancellationToken: cancellationToken);

            foreach (var contact in userContactsList)
            {
                var newContact = command.Contacts
                    .FirstOrDefault(x => x.ResourceName == contact.ContactType.Name );

                if (newContact != null)
                {
                    contact.UpdateContactInfo(newContact.ResourceInfo);
                }
            }

            DbContext.Set<UserContact>().UpdateRange(userContactsList);
            await DbContext.SaveChangesAsync(cancellationToken);

            return command.Contacts;
        }
    }
}