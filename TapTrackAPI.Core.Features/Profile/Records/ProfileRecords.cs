using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TapTrackAPI.Core.Features.Profile.Records
{
    public record UserProfile(string ProfileImageLink, string UserName, string UserEmail);
    public record ChangeUserNameDto(string NewUserName);
    public record UpdateProfileImageQuery(IFormFile Image);
    public record UserProjects(Dictionary<string, string> ProjectPosition);
}