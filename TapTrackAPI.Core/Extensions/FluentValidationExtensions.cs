using System.Net;
using FluentValidation;
using JetBrains.Annotations;

namespace TapTrackAPI.Core.Extensions
{
    [UsedImplicitly]
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this
                IRuleBuilderOptions<T, TProperty> rule,
            HttpStatusCode errorCode)
        {
            return rule.WithErrorCode(((int) errorCode).ToString());
        }
    }
}