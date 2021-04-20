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
            List<ContactInformationDto>>
    {
        public UpdateContactsInfoHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<List<ContactInformationDto>> Handle(UpdateContactInfoCommand command, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);


            var userContactsList = user.UserContacts;

            if (userContactsList != null)
            {
                foreach (var contact in userContactsList)
                {
                    var newContact = command.Contacts
                        .FirstOrDefault(x => x.ResourceName == contact.ContactType.ToString("G"));

                    if (newContact != null)
                    {
                        contact.UpdateContactInfo(newContact.ResourceInfo);
                    }
                }

                DbContext.Set<UserContact>().UpdateRange(userContactsList);
                await DbContext.SaveChangesAsync(cancellationToken);
            }

            return command.Contacts;
        }
    }
}