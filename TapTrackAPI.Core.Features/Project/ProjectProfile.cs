using AutoMapper;

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