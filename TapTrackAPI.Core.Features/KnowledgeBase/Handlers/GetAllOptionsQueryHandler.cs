using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;
using TapTrackAPI.Core.Features.KnowledgeBase.Queries;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Handlers
{
    public class GetAllOptionsQueryHandler : RequestHandlerBase, IRequestHandler<GetAllOptionsQuery, List<OptionDto>>
    {
        protected UserManager<User> UserManager { get; }

        public GetAllOptionsQueryHandler(DbContext context, IMapper mapper, UserManager<User> userManager) : base(
            context,
            mapper)
        {
            UserManager = userManager;
        }

        public async Task<List<OptionDto>> Handle(GetAllOptionsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.AppUser);
            return await Context
                .Set<Entities.Project>()
                .Where(x => x.Team.Any(y => y.UserId == userId))
                .ProjectTo<OptionDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}