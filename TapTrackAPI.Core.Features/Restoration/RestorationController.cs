using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Restoration.PasswordReset;
using TapTrackAPI.Core.Features.Restoration.SendCode;
using TapTrackAPI.Core.Features.Restoration.VerifyCode;

namespace TapTrackAPI.Core.Features.Restoration
{
    public class RestorationController : ApiBaseController
    {
        public RestorationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> SendCode([FromBody] SendCodeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CheckCode")]
        public async Task<IActionResult> CheckCode([FromBody] VerifyCodeCommand command)
        {
            var result = await Mediator.Send(command);
            return result == null ? BadRequest() : Ok(result);
        }

        [HttpPost("Password")]
        public async Task<IActionResult> SendNewPassword([FromBody] PasswordResetCommand command)
        {
            var result = await Mediator.Send(command);
            return result == null ? BadRequest() : Ok(result);
        }
    }
}