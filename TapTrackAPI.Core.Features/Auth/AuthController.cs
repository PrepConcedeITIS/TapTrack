using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
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
        private readonly DbContext _dbContext;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,
            IJwtTokenGenerator tokenGenerator, IMediator mediator, DbContext dbContext)
            : base(mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserInput input)
        {
            var user = new User(input.Email);

            var result = await _userManager.CreateAsync(user, input.Password);
            var userId = await _userManager.GetUserIdAsync(user);
            var guidUserId = new Guid(userId);

            var contactTypes = _dbContext.Set<ContactType>().AsQueryable();

            await _dbContext.Set<UserContact>().AddRangeAsync(
                new UserContact(guidUserId, String.Empty, false,
                    contactTypes.First(x => x.Name == ContactTypeConstants.TelegramName).Id),
                new UserContact(guidUserId, String.Empty, false, 
                    contactTypes.First(x => x.Name == ContactTypeConstants.DiscordName).Id),
                new UserContact(guidUserId, String.Empty, false, 
                    contactTypes.First(x => x.Name == ContactTypeConstants.SkypeName).Id),
                new UserContact(guidUserId, String.Empty, false, 
                    contactTypes.First(x => x.Name == ContactTypeConstants.GitHubName).Id));

            await _dbContext.SaveChangesAsync();

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