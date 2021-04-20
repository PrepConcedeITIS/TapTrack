using FluentValidation;

namespace TapTrackAPI.Core.Features.Auth.Base
{
    public class UserInputValidator<T> : AbstractValidator<T>
        where T : IHasUserInput
    {
        public UserInputValidator()
        {
            RuleFor(x => x.UserInput.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .EmailAddress().WithMessage("Email incorrect format");

            RuleFor(x => x.UserInput.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .MinimumLength(7).WithMessage("Minimum length of password is 7 characters");
        }
    }
}