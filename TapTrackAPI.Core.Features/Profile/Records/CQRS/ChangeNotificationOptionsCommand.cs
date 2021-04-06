﻿using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record ChangeNotificationOptionsCommand(bool Option, ClaimsPrincipal ClaimsPrincipal = null): 
        RecordBase<bool>(ClaimsPrincipal);
}