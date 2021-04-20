using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    public class GetUserProjectsHandler : ProfileHandlerWithDbContextBase<GetUserProjectsQuery, List<UserProjectDto>>
    {
        public GetUserProjectsHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager, dbContext)
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