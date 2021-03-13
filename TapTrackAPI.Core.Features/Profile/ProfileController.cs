using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Data;

namespace TapTrackAPI.Core.Features.Profile
{
    public class ProfileController : AuthorizedApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;     
        
        public ProfileController(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Ok();
        }
    }
}