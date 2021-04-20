using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    public class UpdateContactInfoValidator : ProfileAbstractValidatorBase<UpdateContactInfoCommand>
    {
        public UpdateContactInfoValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleForEach(x => x.Contacts)
                .Must(x => IsValidStringInput(x.ResourceInfo, 4, 50))
                .WithMessage($"Incorrect input");
        }
    }
}