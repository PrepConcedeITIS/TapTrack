using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.List
{
    public class GetProjectsListQuery : IRequest<List<ProjectDto>>, IHasClaims
    {
        public GetProjectsListQuery(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
        }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}