using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class UpdateProfileImageHandler : ProfileHandlerWithDbContextBase<UpdateProfileImageCommand, UserProfileDto>
    {
        private readonly IImageUploadService _imageUploadService;

        public UpdateProfileImageHandler(UserManager<User> userManager, DbContext dbContext,
            IImageUploadService imageUploadService) : base(userManager, dbContext)
        {
            _imageUploadService = imageUploadService;
        }

        public override async Task<UserProfileDto> Handle(UpdateProfileImageCommand command, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);

            var imageUrl = await _imageUploadService.UploadUserProfileImage(command.Image, user.Id.ToString());

            user.UpdateProfileImage(imageUrl);
            DbContext.Set<User>().Update(user);

            await DbContext.SaveChangesAsync();

            return new UserProfileDto(user.ProfileImageUrl,user.UserName, user.Email);
        }
    }
}