using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record ChangeUserNameCommand(string NewUserName, ClaimsPrincipal ClaimsPrincipal) : 
        RecordBase<bool>(ClaimsPrincipal);
}