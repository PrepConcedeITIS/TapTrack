using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Project.List
{
    [UsedImplicitly]
    public class GetProjectsListAsyncHandler : IRequestHandler<GetProjectsListQuery, List<ProjectDto>>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GetProjectsListAsyncHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserIdGuid(request.ClaimsPrincipal);
            var projectsWhereMember = await _dbContext.Set<TeamMember>()
                .Where(x => x.UserId == userId)
                .Select(x => x.Project)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
            return projectsWhereMember;
        }
    }
}