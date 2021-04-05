using System;
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
        List<ContactInformationDto>>
    {
        public GetContactInfoHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
        {
        }

        public override async Task<List<ContactInformationDto>> Handle(GetContactInfoQuery query)
        {
            var user = await UserManager.GetUserAsync(query.ClaimsPrincipal);

            /*var userContactsList = user.UserContacts
                .Select(x => new ContactInformationDto(x.ContactType.Name, x.ContactInfo, x.Id))
                .ToList();*/


            var mock = new Dictionary<string, string>()
            {
                {"Telegram", "@test"},
                {"Skype", "@test"},
                {"Discord", "@test"},
                {"Github", "@test"},
            };

            var result = new List<ContactInformationDto>();

            foreach (var pair in mock)
            {
                result.Add(new ContactInformationDto(pair.Key, pair.Value, Guid.NewGuid()));
            }

            return result;
        }
    }
}