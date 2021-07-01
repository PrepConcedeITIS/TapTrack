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
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.KnowledgeBase.GetProjectOptions
{
    [UsedImplicitly]
    public class GetProjectOptionsQueryHandler : BaseHandlerWithUserManager<GetProjectOptionsQuery, List<OptionDto>>
    {
        public GetProjectOptionsQueryHandler(DbContext context, IMapper mapper, UserManager<User> userManager)
            : base(context, mapper, userManager)
        {
        }

        public override async Task<List<OptionDto>> Handle(GetProjectOptionsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.AppUser);
            return await DbContext
                .Set<Entities.Project>()
                .Where(x => x.Team.Any(y => y.UserId == userId))
                .ProjectTo<OptionDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}