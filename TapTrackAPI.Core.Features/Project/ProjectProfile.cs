using AutoMapper;
using TapTrackAPI.Core.Features.Project.Records;

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