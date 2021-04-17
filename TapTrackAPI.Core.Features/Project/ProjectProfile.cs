using AutoMapper;

namespace TapTrackAPI.Core.Features.Project
{
    public class ProjectProfile: AutoMapper.Profile
    {
        public ProjectProfile()
        {
            CreateMap<Entities.Project, ProjectDto>();
        }
    }
}