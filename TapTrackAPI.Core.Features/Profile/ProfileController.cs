using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile
{
    public class ProfileController : AuthorizedApiController
    {
        private readonly IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileDto> _getUserProfileHandler;
        private readonly IAsyncQueryHandler<GetUserProjectsQuery, GetUserProjectsDto> _getUserProjectsHandler;
        private readonly IAsyncQueryHandler<ChangeUserNameCommand, bool> _changeUserNameHandler;
        private readonly IAsyncQueryHandler<UpdateProfileImageCommand, bool> _updateProfileImageHandler;
        private readonly IAsyncQueryHandler<UpdateContactInfoCommand, bool> _updateContactInfoHandler;

        private readonly IAsyncQueryHandler<GetContactInfoQuery, GetContactInformationDto>
            _getContactInformationHandler;

        public ProfileController(
            IMediator mediator, IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileDto> getUserProfileHandler,
            IAsyncQueryHandler<GetUserProjectsQuery, GetUserProjectsDto> getUserProjectsHandler,
            IAsyncQueryHandler<ChangeUserNameCommand, bool> changeUserNameHandler,
            IAsyncQueryHandler<UpdateProfileImageCommand, bool> updateProfileImageHandler,
            IAsyncQueryHandler<GetContactInfoQuery, GetContactInformationDto> getContactInformationHandler, IAsyncQueryHandler<UpdateContactInfoCommand, bool> updateContactInfoHandler)
            : base(mediator)
        {
            _getUserProfileHandler = getUserProfileHandler;
            _getUserProjectsHandler = getUserProjectsHandler;
            _changeUserNameHandler = changeUserNameHandler;
            _updateProfileImageHandler = updateProfileImageHandler;
            _getContactInformationHandler = getContactInformationHandler;
            _updateContactInfoHandler = updateContactInfoHandler;
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

        [HttpGet("contacts")]
        public async Task<IActionResult> GetUserContactInformation()
        {
            return Ok(await _getContactInformationHandler.Handle(new GetContactInfoQuery(HttpContext.User)));
        }

        [HttpPut("updateContactsInfo")]
        public async Task<IActionResult> UpdateContactInfo([FromForm] UpdateContactInfoCommand updateContactInfoCommand)
        {
            var command = updateContactInfoCommand with {ClaimsPrincipal = HttpContext.User};

            return Ok(await _updateContactInfoHandler.Handle(command));
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