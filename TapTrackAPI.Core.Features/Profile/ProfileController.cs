using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Interfaces;
using TapTrackAPI.Data;

namespace TapTrackAPI.Core.Features.Profile
{
    public record UserProfile(string ProfileImageLink, string UserName, string UserEmail);
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

        [HttpPut("uploadProfileImage")]
        public async Task<IActionResult> UploadProfileImage(IFormFile image)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            await _imageService.UploadUserProfileImage(image, user.Id.ToString());

            return Ok();
        }

        [HttpPost("updateUserName")]
        public async Task<IActionResult> UpdateUserName([FromForm]string newName)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName) || newName.Length > 25)
                return BadRequest("Некорректное имя пользователя");

            user.UserName = newName;

            return Ok();
        }
    }
}