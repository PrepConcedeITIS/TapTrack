using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Base;

namespace TapTrackAPI.Core.Features.Project.AddTeamMember
{
    public class AddTeamMemberHandler : BaseCommandHandler, IRequestHandler<AddTeamMemberCommand,ProjectDto>
    {
        public AddTeamMemberHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        
        public Task<ProjectDto> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
        {
            var tm = new TeamMember(request.UserId,request.ProjectId,request.Role);
            DbContext.Set<TeamMember>().Add(tm);
            return Mapper<ProjectDto>()
        }


    }
}