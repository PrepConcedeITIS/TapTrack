﻿using System.Collections.Generic;
using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    public record GetUserProjectsQuery : 
        ProfileRecordBase<List<UserProjectDto>>;
}