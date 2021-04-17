using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Project.Edit.Validators;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    [UsedImplicitly]
    public class ProjectEditValidator : AbstractValidator<ProjectEditCommand>
    {
        public ProjectEditValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.IdVisible).NotEmpty().MaximumLength(7);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.ClaimsPrincipal)
                .SetAsyncValidator(new HasAccessToProjectPropertyValidator(dbContext, userManager));
        }
    }
}