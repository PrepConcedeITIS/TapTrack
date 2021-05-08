using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue
{
    public class IssueProfile : AutoMapper.Profile
    {
        public IssueProfile()
        {
            CreateMap<Entities.Issue, IssueListItemDto>()
                .ForMember(dto => dto.Project, e =>
                    e.MapFrom(m => m.Project.Name))
                .ForMember(dto => dto.Priority, e =>
                    e.MapFrom(m => m.Priority.ToString()))
                .ForMember(dto => dto.State, e =>
                    e.MapFrom(m => m.State.ToString()))
                .ForMember(dto => dto.Creator, e =>
                    e.MapFrom(m => m.Creator.User.UserName))
                .ForMember(dto => dto.Assignee, e =>
                    e.MapFrom(m => m.Assignee.User.UserName))
                .ForMember(dto => dto.Estimate, e =>
                    e.MapFrom(m => m.Estimation.ToString("c")))
                .ForMember(dto => dto.Spent, e =>
                    e.MapFrom(m => m.Spent.ToString("c")));

            CreateMap<Entities.Issue, IssueDetailsDto>()
                .ForMember(dto => dto.Creator, e =>
                    e.MapFrom(m => m.Creator.User.UserName))
                .ForMember(dto => dto.Assignee, e =>
                    e.MapFrom(m => m.Assignee.User.UserName))
                .ForMember(dto => dto.IssueType, e =>
                    e.MapFrom(m => m.IssueType.ToString()))
                .ForMember(dto => dto.Priority, e =>
                    e.MapFrom(m => m.Priority.ToString()))
                .ForMember(dto => dto.Project, e =>
                    e.MapFrom(m => m.Project.Name))
                .ForMember(dto => dto.State, e =>
                    e.MapFrom(m => m.State.ToString()))
                .ForMember(dto => dto.Priority, e =>
                    e.MapFrom(m => m.Priority.ToString()))
                .ForMember(dto => dto.EstimationHours, e =>
                    e.MapFrom(m => m.Estimation.Hours))
                .ForMember(dto => dto.EstimationMinutes, e =>
                    e.MapFrom(m => m.Estimation.Minutes))
                .ForMember(dto => dto.SpentHours, e =>
                    e.MapFrom(m => m.Spent.Hours))
                .ForMember(dto => dto.SpentMinutes, e =>
                    e.MapFrom(m => m.Spent.Minutes))
                .ForMember(dto => dto.Created, e =>
                    e.MapFrom(m => m.Created.Date.ToShortDateString()));
        }
    }
}