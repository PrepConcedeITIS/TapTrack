using System;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Project.Validators;

namespace TapTrackAPI.Core.Features.Project.Get
{
    [UsedImplicitly]
    public class ProjectGetByIdValidator : AbstractValidator<GetProjectByIdQuery>
    {
        public ProjectGetByIdValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(x => x.ProjectId).Must((query, _) => query.ProjectId != Guid.Empty)
                .WithMessage("Project id can't be empty");
            RuleFor(x => x.ClaimsPrincipal)
                .SetAsyncValidator(new HasAccessToProjectPropertyValidator<GetProjectByIdQuery>(dbContext, userManager,
                    new[] {Role.User, Role.Admin}))
                .WithErrorCode("403")
                .WithMessage("You can't touch this project");
        }
    }
}