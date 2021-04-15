using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Restoration.DTO;

namespace TapTrackAPI.Core.Features.Restoration.Handlers
{
    public class NewPasswordHandler : RequestHandlerBase, IRequestHandler<NewPasswordQuery>
    {
        private readonly UserManager<User> _userManager;
        public NewPasswordHandler(UserManager<User> userManager, DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(NewPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserMail);
            if (user != null)
            {
                var result = await _userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddPasswordAsync(user, request.Password);
                }
            }
            return default;
        }
    }
}