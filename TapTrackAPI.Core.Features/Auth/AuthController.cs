using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Auth
{
    public record UserInput(string Email, string Password);

    [AllowAnonymous]
    public class AuthController : ApiBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,
            IJwtTokenGenerator tokenGenerator, IMediator mediator)
            : base(mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserInput input)
        {
            var user = new User(input.Email);
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                return Ok(new {User = user, Token = _tokenGenerator.GenerateToken(user)});
            }
            else
            {
                return BadRequest(new {Input = input, ErrorList = result.Errors.ToArray()});
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetToken([FromBody] UserInput input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            var signInResult =
                await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
            if (signInResult.Succeeded)
                return Ok(new {User = user, Token = _tokenGenerator.GenerateToken(user)});
            return BadRequest();
        }
    }
}
