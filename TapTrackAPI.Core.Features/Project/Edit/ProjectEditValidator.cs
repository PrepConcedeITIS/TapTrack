using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Project.Validators;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    [UsedImplicitly]
    public class ProjectEditValidator : AbstractValidator<ProjectEditCommand>
    {
        public ProjectEditValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(x => x.Name.Trim()).NotEmpty().MaximumLength(30)
                .WithMessage("Max length for project name is 30 characters");
            RuleFor(x => x.IdVisible).NotEmpty().MaximumLength(7)
                .WithMessage("Max length for project shortcut name is 7 characters");
            RuleFor(x => x.Description).MaximumLength(500)
                .WithMessage("Max length for project description is 500 characters");
            RuleFor(x => x.ClaimsPrincipal)
                .SetAsyncValidator(new HasAccessToProjectPropertyValidator<ProjectEditCommand>(dbContext, userManager))
                .WithErrorCode("403")
                .WithMessage("You can't touch this project");
        }
    }
}