using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;
using TapTrackAPI.Core.Interfaces;
using TapTrackAPI.Data;

namespace TapTrackAPI.Core.Features.Profile
{
    public class ProfileController : AuthorizedApiController
    {
        private readonly IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileDto> _getUserProfileHandler;
        private readonly IAsyncQueryHandler<GetUserProjectsQuery, GetUserProjectsDto> _getUserProjectsHandler;
        private readonly IAsyncQueryHandler<ChangeUserNameCommand, bool> _changeUserNameHandler;
        private readonly IAsyncQueryHandler<UpdateProfileImageCommand, bool> _updateProfileImageHandler;

        public ProfileController(
            IMediator mediator, IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileDto> getUserProfileHandler,
            IAsyncQueryHandler<GetUserProjectsQuery, GetUserProjectsDto> getUserProjectsHandler,
            IAsyncQueryHandler<ChangeUserNameCommand, bool> changeUserNameHandler,
            IAsyncQueryHandler<UpdateProfileImageCommand, bool> updateProfileImageHandler)
            : base(mediator)
        {
            _getUserProfileHandler = getUserProfileHandler;
            _getUserProjectsHandler = getUserProjectsHandler;
            _changeUserNameHandler = changeUserNameHandler;
            _updateProfileImageHandler = updateProfileImageHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _getUserProfileHandler.Handle(new GetUserProfileQuery(HttpContext.User)));
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetUserProjects()
        {
            return Ok(await _getUserProjectsHandler.Handle(new GetUserProjectsQuery(HttpContext.User)));
        }

        [HttpPut("uploadProfileImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadProfileImage(
            [FromForm] UpdateProfileImageCommand updateProfileImageCommand)
        {
            //TODO: заменить command            
            return Ok(await _updateProfileImageHandler.Handle(
                new UpdateProfileImageCommand(updateProfileImageCommand.Image, HttpContext.User)));
        }


        [HttpPut("updateUserName")]
        public async Task<IActionResult> UpdateUserName([FromBody] ChangeUserNameCommand changeUserNameCommand)
        {
            return Ok(await _changeUserNameHandler.Handle(new ChangeUserNameCommand(changeUserNameCommand.NewUserName,
                HttpContext.User)));
        }
    }
}