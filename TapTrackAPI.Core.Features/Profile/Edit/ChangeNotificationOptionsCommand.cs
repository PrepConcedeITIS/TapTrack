using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    public record ChangeNotificationOptionsCommand(bool Option): 
        ProfileRecordBase<bool>;
}