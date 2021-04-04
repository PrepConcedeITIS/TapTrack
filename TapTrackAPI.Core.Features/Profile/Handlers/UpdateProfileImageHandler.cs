﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;
using TapTrackAPI.Core.Interfaces;
using TapTrackAPI.Data;

namespace TapTrackAPI.Core.Features.Profile.Handlers
{
    public class UpdateProfileImageHandler : ProfileHandlerWithDbContextBase<UpdateProfileImageCommand, bool>
    {
        private readonly IImageUploadService _imageUploadService;

        public UpdateProfileImageHandler(UserManager<User> userManager, AppDbContext dbContext, IImageUploadService imageUploadService) : base(userManager, dbContext)
        {
            _imageUploadService = imageUploadService;
        }

        public override async Task<bool> Handle(UpdateProfileImageCommand command)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);

            //await _imageUploadService.UploadUserProfileImage(command.Image, user.Id.ToString());
            
            return true;
        }
    }
}