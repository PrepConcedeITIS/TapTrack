using System.Collections.Generic;
using System.Security.Claims;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public record GetContactInfoQuery(ClaimsPrincipal ClaimsPrincipal) :
        RecordBase<List<ContactInformationDto>>(ClaimsPrincipal);
}