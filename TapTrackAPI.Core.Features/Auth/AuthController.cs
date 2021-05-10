using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Auth.Base;
using TapTrackAPI.Core.Features.Auth.Login;
using TapTrackAPI.Core.Features.Auth.Registration;

namespace TapTrackAPI.Core.Features.Auth
{
    [AllowAnonymous]
    public class AuthController : ApiBaseController
    {
        public AuthController(IMediator mediator)
            : base(mediator)
        {
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserInput input)
        {
            var result = await Mediator.Send(new RegistrationCommand(input));
            if (result.Errors.Any())
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetToken([FromBody] UserInput input)
        {
            var result = await Mediator.Send(new LoginCommand(input));
            return result == null
                ? BadRequest()
                : Ok(result);
        }
    }
}