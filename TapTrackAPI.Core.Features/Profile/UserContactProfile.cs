using JetBrains.Annotations;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile
{
    [UsedImplicitly]
    public class UserContactProfile : AutoMapper.Profile
    {
        public UserContactProfile()
        {
            CreateMap<Entities.UserContact, ContactInformationListItemDto>()
                .ForMember(dto => dto.ResourceInfo,
                    e => e.MapFrom(m => m.ContactInfo))
                .ForMember(dto => dto.ResourceName, e => e.MapFrom(m => m.ContactType.Name));
        }
    }
}