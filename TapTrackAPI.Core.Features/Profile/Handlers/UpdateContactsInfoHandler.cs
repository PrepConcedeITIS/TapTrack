using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Handlers
{
    public class
        UpdateContactsInfoHandler : ProfileHandlerWithDbContextBase<UpdateContactInfoCommand,
            List<ContactInformationDto>>
    {
        public UpdateContactsInfoHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<List<ContactInformationDto>> Handle(UpdateContactInfoCommand command)
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
                await DbContext.SaveChangesAsync();
            }

            return command.Contacts;
        }
    }
}