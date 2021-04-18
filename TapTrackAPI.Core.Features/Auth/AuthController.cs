using System;
using System.Linq;
using System.Net.Mail;
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
        private readonly IMailSender _test;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,
            IJwtTokenGenerator tokenGenerator, IMediator mediator, IMailSender test)
            : base(mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _test = test;
        }
        [HttpGet("test")]
        public async Task<IActionResult> MailSend()
        {
            var mailFrom = new MailAddress("taptrackproject@gmail.com");
            var mailTo = new MailAddress("rnasybullin2013@litsey2.ru");
            var message = new MailMessage(mailFrom, mailTo);
            message.Body = "ssss";
            await _test.SendMessageAsync(message);
            DateTime date1 = new DateTime(2010, 1, 10, 4, 4, 4);
            DateTime date2 = new DateTime(2010, 1, 12, 9, 6, 40);
            TimeSpan a = date2 - date1;
            var jopa = a.Days + " days, " + a.Hours + " hours";
            return Ok(a);

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
