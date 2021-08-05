using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Project.Base;
using TapTrackAPI.Core.Features.Project.Validators;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    [UsedImplicitly]
    public class ProjectEditValidator : ProjectValidatorBase<ProjectEditCommand>
    {
        public ProjectEditValidator(DbContext dbContext, UserManager<User> userManager) : base()
        {
            RuleFor(x => x)
                .MustAsync(async (command, token) =>
                {
                    var already = await dbContext.Set<Entities.Project>()
                        .FirstOrDefaultAsync(x => x.IdVisible == command.IdVisible && x.Id != command.ProjectId,
                            cancellationToken: token);
                    return already == null;
                })
                .WithMessage("Project with the same shortcut name already exist");
            RuleFor(x => x.ClaimsPrincipal)
                .SetAsyncValidator(new HasAccessToProjectPropertyValidator<ProjectEditCommand>(dbContext, userManager,
                    new[] {Role.Admin}))
                .WithErrorCode("403")
                .WithMessage("You can't touch this project");
        }
    }
}