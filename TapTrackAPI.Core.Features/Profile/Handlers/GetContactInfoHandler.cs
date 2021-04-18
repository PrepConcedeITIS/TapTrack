using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
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
            
            var userContactsList = DbContext.Set<UserContact>()
                .Where(x => x.UserId == user.Id)
                .Select(x => new ContactInformationDto(x.ContactType.ToString("G"), x.ContactInfo))
                .ToList();

            return userContactsList;
        }
    }
}