using TapTrackAPI.Core.Features.Invitation.Dto;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class InvitationProfile : AutoMapper.Profile
    {
        public InvitationProfile()
        {
            CreateMap<Entities.Invitation, InvitationDtoBase>()
                .ForMember(x => x.UserName,
                    (d) => d.MapFrom(x => x.User.UserName))
                .ForMember(x => x.ProjectName,
                    (d) => d.MapFrom(x => x.Project.Name))
                .ForMember(x => x.Role,
                    (d) => d.MapFrom(x => x.InvitationRole.ToString()))
                .ForMember(x => x.Status,
                    (d) => d.MapFrom(x => x.InvitationState.ToString()));

            CreateMap<Entities.Invitation, InvitationGridDto>()
                .IncludeBase<Entities.Invitation, InvitationDtoBase>();
            CreateMap<Entities.Invitation, InvitationDtoDetailed>()
                .IncludeBase<Entities.Invitation, InvitationDtoBase>();
        }
    }
}