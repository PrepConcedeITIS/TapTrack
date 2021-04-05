using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record ChangeUserNameCommand(string NewUserName, ClaimsPrincipal ClaimsPrincipal) : 
        RecordBase<UserProfileDto>(ClaimsPrincipal);
}