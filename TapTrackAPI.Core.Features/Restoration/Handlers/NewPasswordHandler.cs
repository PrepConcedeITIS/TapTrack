using System;
using System.Linq;
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
    public class NewPasswordHandler : RequestHandlerBase, IRequestHandler<NewPasswordQuery, RestorationCode>
    {
        private readonly UserManager<User> _userManager;
        public NewPasswordHandler(UserManager<User> userManager, DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _userManager = userManager;
        }

        public async Task<RestorationCode> Handle(NewPasswordQuery request, CancellationToken cancellationToken)
        {
            var dbCode = Context.Set<Entities.RestorationCode>()
                .Where(x => x.Email == request.UserMail)
                .OrderByDescending(x => x.CreationDate)
                .FirstOrDefault();
            var userCode = request.UserCode;
            if (dbCode != null && dbCode.Code == userCode && DateTime.Compare(dbCode.ExpirationDate,DateTime.Now) > 0)
            {
                dbCode.CodeIsUsed();
                var user = await _userManager.FindByEmailAsync(request.UserMail);
                if (user == null) return default;
                var result = await _userManager.RemovePasswordAsync(user);
                if (!result.Succeeded) return default;
                result = await _userManager.AddPasswordAsync(user, request.UserPassword);
                if (!result.Succeeded) return default;
                await Context.SaveChangesAsync(cancellationToken);
                return dbCode;
            }
            return default;
        }
    }
}