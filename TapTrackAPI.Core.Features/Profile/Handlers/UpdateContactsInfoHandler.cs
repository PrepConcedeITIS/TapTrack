using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Handlers
{
    public class UpdateContactsInfoHandler: ProfileHandlerWithDbContextBase<UpdateContactInfoCommand, ContactInformationDto>
    {
        public UpdateContactsInfoHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager, dbContext)
        {
        }

        public override Task<ContactInformationDto> Handle(UpdateContactInfoCommand input)
        {
            throw new System.NotImplementedException();
        }
    }
}