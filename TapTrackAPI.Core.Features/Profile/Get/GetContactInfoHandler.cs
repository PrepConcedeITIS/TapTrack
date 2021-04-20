using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetContactInfoHandler : ProfileHandlerWithDbContextBase<GetContactInfoQuery,
        List<ContactInformationDto>>
    {
        public GetContactInfoHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager, dbContext)
        {
        }

        public override async Task<List<ContactInformationDto>> Handle(GetContactInfoQuery request,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(request.ClaimsPrincipal);

            var userContactsList = DbContext.Set<UserContact>()
                .Where(x => x.UserId == user.Id)
                .Select(x => new ContactInformationDto(x.ContactType.ToString("G"), x.ContactInfo))
                .ToList();

            return userContactsList;
        }
    }
}