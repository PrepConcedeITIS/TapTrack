using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace TapTrackAPI.Core.Base
{
    [UsedImplicitly]
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
            var exceptionMessage = exception.Message;
            
            var hasForbiddenCode = exception.Errors.FirstOrDefault(x => x.ErrorCode== "403");
            if (hasForbiddenCode != null)
            {
                context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                exceptionMessage = hasForbiddenCode.ErrorMessage;
            }

            return context.Response.WriteAsync(exceptionMessage);
        }
    }
}