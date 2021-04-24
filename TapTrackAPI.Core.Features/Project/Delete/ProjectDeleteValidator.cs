using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Project.Validators;

namespace TapTrackAPI.Core.Features.Project.Delete
{
    public class ProjectDeleteValidator : AbstractValidator<ProjectDeleteCommand>
    {
        public ProjectDeleteValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(x => x.ClaimsPrincipal)
                .SetAsyncValidator(
                    new HasAccessToProjectPropertyValidator<ProjectDeleteCommand>(dbContext, userManager))
                .WithErrorCode("403")
                .WithMessage("You can't touch this project");
        }
    }
}