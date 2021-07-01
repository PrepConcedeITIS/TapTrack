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
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Project.List
{
    [UsedImplicitly]
    public class GetProjectsListAsyncHandler : BaseHandlerWithUserManager<GetProjectsListQuery, List<ProjectDto>>
    {
        public GetProjectsListAsyncHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager) : base(
            dbContext, mapper, userManager)
        {
        }

        public override async Task<List<ProjectDto>> Handle(GetProjectsListQuery request,
            CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.ClaimsPrincipal);
            var projectsWhereMember = await DbContext.Set<TeamMember>()
                .Where(x => x.UserId == userId)
                .Select(x => x.Project)
                .ProjectTo<ProjectDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
            return projectsWhereMember;
        }
    }
}