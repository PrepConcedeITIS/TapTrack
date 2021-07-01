using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class UpdateContactsInfoHandler
        : BaseHandlerWithUserManager<UpdateContactInfoCommand, List<ContactInformationListItemDto>>
    {
        public UpdateContactsInfoHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager) : base(
            dbContext, mapper, userManager)
        {
        }

        public override async Task<List<ContactInformationListItemDto>> Handle(UpdateContactInfoCommand command,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);


            var userContactsList = await DbContext.Set<UserContact>()
                .Where(x => x.UserId == user.Id)
                .Include(x => x.ContactType)
                .ToListAsync(cancellationToken: cancellationToken);

            foreach (var contact in userContactsList)
            {
                var newContact = command.Contacts
                    .FirstOrDefault(x => x.ResourceName == contact.ContactType.Name);

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