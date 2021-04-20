using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetNotificationOptionsValidator : AbstractValidator<GetNotificationOptionsQuery>
    {
        private readonly DbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public GetNotificationOptionsValidator(DbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<GetNotificationOptionsQuery> context, CancellationToken cancellation = new CancellationToken())
        {
            var user = await _userManager.GetUserAsync(context.InstanceToValidate.ClaimsPrincipal);

            var contacts = await _dbContext.Set<UserContact>()
                .Where(x => x.User == user)
                .FirstOrDefaultAsync(x => x.ContactType == ContactType.Telegram, cancellationToken: cancellation);

            if (contacts == null)
                return new ValidationResult(new [] { new ValidationFailure("UserContact", "Telegram contact doesn't exits") });
            
            if(string.IsNullOrEmpty(contacts.ContactInfo) || string.IsNullOrWhiteSpace(contacts.ContactInfo))
                return new ValidationResult(new [] { new ValidationFailure("ContactInfo", "Telegram contact doesn't exits") });
            
            return await base.ValidateAsync(context, cancellation);
        }
    }
}