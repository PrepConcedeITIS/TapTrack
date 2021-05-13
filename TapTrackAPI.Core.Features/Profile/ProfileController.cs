using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Profile.DeleteTelegramConnection;
using TapTrackAPI.Core.Features.Profile.Edit;
using TapTrackAPI.Core.Features.Profile.Get;
using TapTrackAPI.Core.Features.Profile.GetTelegramInfo;

namespace TapTrackAPI.Core.Features.Profile
{
    public class ProfileController : AuthorizedApiController
    {
        public ProfileController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetUserProfileQuery {ClaimsPrincipal = User}));
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetUserProjects()
        {
            return Ok(await Mediator.Send(new GetUserProjectsQuery {ClaimsPrincipal = User}));
        }

        [HttpGet("contacts")]
        public async Task<IActionResult> GetUserContactInformation()
        {
            return Ok(await Mediator.Send(new GetContactInfoQuery {ClaimsPrincipal = User}));
        }

        [HttpGet("notificationOptions")]
        public async Task<IActionResult> GetUserNotificationOptions()
        {
            return Ok(await Mediator.Send(new GetNotificationOptionsQuery {ClaimsPrincipal = User}));
        }

        [HttpPut("updateContactsInfo")]
        public async Task<IActionResult> UpdateContactInfo([FromBody] UpdateContactInfoCommand updateContactInfoCommand)
        {
            var command = updateContactInfoCommand with {ClaimsPrincipal = User};

            return Ok(await Mediator.Send(command));
        }

        [HttpPost("uploadProfileImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadProfileImage(
            [FromForm] UpdateProfileImageCommand updateProfileImageCommand)
        {
            updateProfileImageCommand.ClaimsPrincipal = User;

            return Ok(await Mediator.Send(updateProfileImageCommand));
        }

        [HttpPut("changeNotificationOption")]
        public async Task<IActionResult> ChangeNotificationOption(
            [FromBody] ChangeNotificationOptionsCommand changeNotificationOptionsCommand)
        {
            var command = changeNotificationOptionsCommand with {ClaimsPrincipal = User};

            return Ok(await Mediator.Send(command));
        }

        [HttpPut("updateUserName")]
        public async Task<IActionResult> UpdateUserName([FromBody] ChangeUserNameCommand changeUserNameCommand)
        {
            var command = changeUserNameCommand with {ClaimsPrincipal = User};

            return Ok(await Mediator.Send(command));
        }

        [HttpGet("telegramInfo")]
        public async Task<IActionResult> GetTelegramNotificationsInfo()
        {
            return Ok(await Mediator.Send(new GetTelegramInfoQuery(User)));
        }

        [HttpDelete("telegramInfo")]
        public async Task<IActionResult> DeleteTelegramConnection()
        {
            var result = await Mediator.Send(new DeleteTelegramConnectionCommand(User));
            return result == null ? BadRequest() : Ok();
        }
    }
}