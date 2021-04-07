using AutoMapper;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Handlers
{
    public class IssueDetailsProfile : AutoMapper.Profile
    {
        public IssueDetailsProfile()
        {
            CreateMap<Entities.Issue, IssueDetailsDto>()
                .ForMember(x => x.IssueType,
                    ex => ex.MapFrom(t => t.IssueType.ToString()))
                .ForMember(x => x.Creator,
                    ex => ex.MapFrom(t => t.Creator.User.UserName))
                .ForMember(x => x.Assignee,
                    ex => ex.MapFrom(t => t.Assignee.User.UserName))
                .ForMember(x => x.Priority,
                    ex => ex.MapFrom(t => t.Priority.ToString()));
        }
    }
}