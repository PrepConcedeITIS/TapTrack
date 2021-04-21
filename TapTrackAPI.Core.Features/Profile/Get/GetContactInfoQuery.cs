using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    public record GetContactInfoQuery : ProfileRecordBase<List<ContactInformationListItemDto>>;
}