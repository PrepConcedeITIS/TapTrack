using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class ChangeUserNameValidator : ProfileAbstractValidatorBase<ChangeUserNameCommand>
    {
        public ChangeUserNameValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(x => x.NewUserName)
                .Must(x => IsValidStringInput(x, 4, 25))
                .WithMessage("Invalid name specified");
        }
    }
}