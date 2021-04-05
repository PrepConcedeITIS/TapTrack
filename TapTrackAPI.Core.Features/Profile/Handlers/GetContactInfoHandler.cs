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
    public class GetContactInfoHandler : ProfileHandlerWithDbContextBase<GetContactInfoQuery,
            ContactInformationDto>
    {
        public GetContactInfoHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<ContactInformationDto> Handle(GetContactInfoQuery query)
        {
            var user = await UserManager.GetUserAsync(query.ClaimsPrincipal);

            /*var userContacts = user.UserContacts
                .Select(x => new
                {
                    ContactName = x.ContactType.Name,
                    ContactInfo = x.ContactInfo
                })
                .ToDictionary(x => x.ContactName, x => x.ContactInfo);*/


            var mock = new Dictionary<string, string>()
            {
                {"Telegram", "@test"},
                {"Skype", "@test"},
                {"Discord", "@test"},
                {"Github", "@test"},
            };

            return new ContactInformationDto(mock);
        }
    }
}