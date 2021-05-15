namespace TapTrackAPI.Core.Features.Invitation
{
    public class InvitationProfile : AutoMapper.Profile
    {
        public InvitationProfile()
        {
            CreateMap<Entities.Invitation, InvitationDto>()
                .ForMember(x => x.UserName,
                    (d) => d.MapFrom(x => x.User.UserName))
                .ForMember(x => x.ProjectName,
                    (d) => d.MapFrom(x => x.Project.Name))
                .ForMember(x => x.InvitationRole,
                    (d) => d.MapFrom(x => x.InvitationRole.ToString()))
                .ForMember(x => x.InvitationState,
                    (d) => d.MapFrom(x => x.InvitationState.ToString()));
        }
    }
}