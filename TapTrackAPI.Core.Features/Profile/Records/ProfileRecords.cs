namespace TapTrackAPI.Core.Features.Profile.Records
{
    public record UserProfile(string ProfileImageLink, string UserName, string UserEmail);
    public record ChangeUserNameDto(string NewUserName);
}