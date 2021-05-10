using FluentValidation;

namespace TapTrackAPI.Core.Features.Profile.Base
{
    public class ProfileAbstractValidatorBase<T> : AbstractValidator<T>
    {
        protected bool IsValidStringInput(string input, int minInputLength, int maxInputLength)
        {
            return !string.IsNullOrEmpty(input) && 
                   !string.IsNullOrWhiteSpace(input) && 
                   input.Length >= minInputLength &&
                   input.Length <= maxInputLength;
        }
    }
}