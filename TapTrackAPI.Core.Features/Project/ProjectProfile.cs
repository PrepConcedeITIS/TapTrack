using AutoMapper;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectProfile: Profile
    {
        public ProjectProfile()
        {
            CreateMap<Entities.Project, ProjectDto>();
        }
    }
}