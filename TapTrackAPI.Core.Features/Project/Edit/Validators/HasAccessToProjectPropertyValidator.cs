using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Project.Edit.Validators
{
    public class HasAccessToProjectPropertyValidator : AsyncPropertyValidator<ProjectEditCommand, ClaimsPrincipal>
    {
        private readonly DbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public HasAccessToProjectPropertyValidator(DbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            Name = "Access to this project is denied";
        }

        public override async Task<bool> IsValidAsync(ValidationContext<ProjectEditCommand> context,
            ClaimsPrincipal value,
            CancellationToken cancellation)
        {
            var projectId = context.InstanceToValidate.ProjectId;
            var userId = _userManager.GetUserIdGuid(value);
            var teamMember = await _dbContext.Set<TeamMember>()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId,
                    cancellationToken: cancellation);
            var projectCreator = (await _dbContext.Set<Entities.Project>()
                    .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken: cancellation))
                ?.CreatorId;
            var hasAccess = teamMember != null || (projectCreator == userId);

            return hasAccess;
        }

        public override string Name { get; }
    }
}