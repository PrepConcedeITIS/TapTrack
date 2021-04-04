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
    public class GetUserProjectsHandler : ProfileHandlerWithDbContextBase<GetUserProjectsQuery, GetUserProjectsDto>
    {
        public GetUserProjectsHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager, dbContext)
        {
        }

        public override async Task<GetUserProjectsDto> Handle(GetUserProjectsQuery query)
        {
            var mock = new Dictionary<string, string>()
            {
                {"Project 1", "developer"},
                {"Project 2", "developer"},
                {"Project 3", "developer"},
                {"Project 4", "developer"},
            };

            var user = UserManager.GetUserAsync(query.ClaimsPrincipal);

            /*var userProject = DbContext.Set<Entities.Project>()
                .AsQueryable()
                .Where(x => x.Team
                    .Select(y => y.User).Contains(user))
                .Select(x => new
                {
                    Project = x.Name,
                    Position = x.Team.First(y => y.User == user).Role
                });*/

            return new GetUserProjectsDto(mock);
        }
    }
}