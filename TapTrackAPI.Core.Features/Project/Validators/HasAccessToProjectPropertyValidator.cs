using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.Project.Base;

namespace TapTrackAPI.Core.Features.Project.Validators
{
    public class HasAccessToProjectPropertyValidator<TProjectCommand>
        : AsyncPropertyValidator<TProjectCommand, ClaimsPrincipal>
        where TProjectCommand : IHasProjectId
    {
        private readonly DbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly string[] _allowedRoles;

        public HasAccessToProjectPropertyValidator(DbContext dbContext, UserManager<User> userManager,
            Role[] allowedRoles)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _allowedRoles = allowedRoles.Select(x => x.ToString()).ToArray();
            Name = "Access to this project is denied";
        }

        public override async Task<bool> IsValidAsync(ValidationContext<TProjectCommand> context,
            ClaimsPrincipal value,
            CancellationToken ct)
        {
            var projectId = context.InstanceToValidate.ProjectId;
            var userId = _userManager.GetUserIdGuid(value);
            var teamMember = await _dbContext.Set<TeamMember>()
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId && x.ProjectId == projectId && _allowedRoles.Contains(x.Role), ct);
            var projectCreator = (await _dbContext.Set<Entities.Project>()
                    .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken: ct))
                ?.CreatorId;
            var hasAccess = teamMember != null || (projectCreator == userId);

            return hasAccess;
        }

        public override string Name { get; }
    }
}