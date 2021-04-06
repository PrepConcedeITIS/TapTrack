﻿using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record GetNotificationOptionsQuery(ClaimsPrincipal ClaimsPrincipal): 
        RecordBase<bool>(ClaimsPrincipal);
}