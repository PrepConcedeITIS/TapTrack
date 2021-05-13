using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetContactInfoHandler : ProfileHandlerWithDbContextBase<GetContactInfoQuery,
        List<ContactInformationListItemDto>>
    {
        private readonly IMapper _mapper;
        
        public GetContactInfoHandler(UserManager<User> userManager, DbContext dbContext, IMapper mapper) : base(userManager, dbContext)
        {
            _mapper = mapper;
        }

        public override async Task<List<ContactInformationListItemDto>> Handle(GetContactInfoQuery request,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(request.ClaimsPrincipal);

            var userContactsList = DbContext.Set<UserContact>()
                .Where(x => x.UserId == user.Id)
                .ProjectTo<ContactInformationListItemDto>(_mapper.ConfigurationProvider)
                .ToList();

            return userContactsList;
        }
    }
}