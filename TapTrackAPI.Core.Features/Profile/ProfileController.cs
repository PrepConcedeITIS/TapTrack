using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Records;
using TapTrackAPI.Core.Interfaces;
using TapTrackAPI.Data;

namespace TapTrackAPI.Core.Features.Profile
{
    public class ProfileController : AuthorizedApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IImageUploadService _imageService;
        private readonly AppDbContext _context;

        public ProfileController(UserManager<User> userManager, AppDbContext context, IImageUploadService imageService)
        {
            _userManager = userManager;
            _context = context;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return BadRequest("Пользователь не найден");

            return Ok(new UserProfile("здесь должна быть ссылка на image", user.UserName, user.Email));
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetUserProjects()
        {
            var mock = new Dictionary<string, string>()
            {
                {"Project 1", "developer"},
                {"Project 2", "developer"},
                {"Project 3", "developer"},
                {"Project 4", "developer"},
            };

            var user = await _userManager.GetUserAsync(HttpContext.User);
            /*var userProject = _context.Projects
                .Where(x => x.Team
                    .Select(y => y.User).Contains(user))
                .Select(x => new
                {
                    Project = x.Name,
                    Position = x.Team.First(y => y.User == user).Role
                });*/

            return Ok(mock);
        }

        [HttpPut("uploadProfileImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadProfileImage([FromForm] UpdateProfileImageQuery updateProfileImageQuery)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            await _imageService.UploadUserProfileImage(updateProfileImageQuery.Image, user.Id.ToString());

            //TODO: добавить поля для пользователя

            return Ok();
        }


        [HttpPut("updateUserName")]
        public async Task<IActionResult> UpdateUserName([FromBody] ChangeUserNameDto newUserName)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (string.IsNullOrEmpty(newUserName.NewUserName) || string.IsNullOrWhiteSpace(newUserName.NewUserName) ||
                newUserName.NewUserName.Length > 25)
                return BadRequest("Некорректное имя пользователя");

            user.UserName = newUserName.NewUserName;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}