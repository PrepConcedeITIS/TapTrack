using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetContactInfoHandler
        : BaseHandlerWithUserManager<GetContactInfoQuery, List<ContactInformationListItemDto>>
    {
        public GetContactInfoHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager) : base(
            dbContext, mapper, userManager)
        {
        }

        public override async Task<List<ContactInformationListItemDto>> Handle(GetContactInfoQuery request,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(request.ClaimsPrincipal);

            var userContactsList = DbContext.Set<UserContact>()
                .Where(x => x.UserId == user.Id)
                .ProjectTo<ContactInformationListItemDto>(Mapper.ConfigurationProvider)
                .ToList();

            return userContactsList;
        }
    }
}