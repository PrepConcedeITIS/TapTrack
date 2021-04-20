﻿using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    public record GetUserProfileQuery : 
        ProfileRecordBase<UserProfileDto>;
}