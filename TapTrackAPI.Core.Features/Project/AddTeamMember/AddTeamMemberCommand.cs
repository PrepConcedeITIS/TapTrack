using System;
using MediatR;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Project.Base;

namespace TapTrackAPI.Core.Features.Project.AddTeamMember
{
    public class AddTeamMemberCommand :IRequest<ProjectDto>, IHasProjectId
    {
        public Guid ProjectId { get; init; }
        public Guid UserId { get; set; }
        public Role Role { get; set; }
    }
}