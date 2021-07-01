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

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetUserProjectsHandler : BaseHandlerWithUserManager<GetUserProjectsQuery, List<UserProjectDto>>
    {
        public GetUserProjectsHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager)
        {
        }

        public override async Task<List<UserProjectDto>> Handle(GetUserProjectsQuery query,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(query.ClaimsPrincipal);

            var userProject = DbContext.Set<Entities.Project>()
                .AsQueryable()
                .Where(x => x.Team
                    .Select(y => y.User).Contains(user))
                .Select(x => new UserProjectDto(x.Name,
                    x.Team.First(y => y.User == user).Role)
                );

            return userProject.ToList();
        }
    }
}