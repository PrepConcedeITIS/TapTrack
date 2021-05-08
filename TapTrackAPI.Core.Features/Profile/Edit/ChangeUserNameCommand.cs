using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    public record ChangeUserNameCommand(string NewUserName) : 
        ProfileRecordBase<UserProfileDto>;
}