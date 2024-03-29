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

namespace TapTrackAPI.Core.Features.Issue.Get
{
    [UsedImplicitly]
    public class GetIssueListHandler : BaseHandler<GetIssueListQuery, List<IssueListItemDto>>
    {
        private readonly UserManager<User> _userManager;

        public GetIssueListHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper)
        {
            _userManager = userManager;
        }

        public override async Task<List<IssueListItemDto>> Handle(GetIssueListQuery input, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserIdGuid(input.ClaimsPrincipal);
            var issues = await DbContext
                .Set<Entities.Issue>()
                .Where(issue=>issue.Project.Team.Any(member => member.UserId == userId))
                .ProjectTo<IssueListItemDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return issues;
        }
    }
}