using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Dto;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class UpdateProfileImageHandler : BaseHandlerWithUserManager<UpdateProfileImageCommand, UserProfileDto>
    {
        private readonly IImageUploadService _imageUploadService;


        public UpdateProfileImageHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager,
            IImageUploadService imageUploadService) : base(dbContext, mapper, userManager)
        {
            _imageUploadService = imageUploadService;
        }

        public override async Task<UserProfileDto> Handle(UpdateProfileImageCommand command,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);

            var imageUrl = await _imageUploadService.UploadUserProfileImage(command.Image, user.Id.ToString());

            user.UpdateProfileImage(imageUrl);
            DbContext.Set<User>().Update(user);

            await DbContext.SaveChangesAsync(cancellationToken);

            return new UserProfileDto(user.ProfileImageUrl, user.UserName, user.Email);
        }
    }
}