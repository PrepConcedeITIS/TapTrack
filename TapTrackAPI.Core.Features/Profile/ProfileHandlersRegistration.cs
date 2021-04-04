using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Profile.Handlers;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile
{
    public static class ProfileHandlersRegistration
    {
        public static void RegisterProfileHandlers(this IServiceCollection services)
        {
            services.AddScoped<IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileDto>, GetUserProfileHandler>();
            services.AddScoped<IAsyncQueryHandler<GetUserProjectsQuery, GetUserProjectsDto>, GetUserProjectsHandler>();
            services.AddScoped<IAsyncQueryHandler<ChangeUserNameCommand, bool>, ChangeUserNameHandler>();
            services.AddScoped<IAsyncQueryHandler<UpdateProfileImageCommand, bool>, UpdateProfileImageHandler>();
            services.AddScoped<IAsyncQueryHandler<GetContactInfoQuery, GetContactInformationDto>,
                    GetContactInfoHandler>();
            services.AddScoped<IAsyncQueryHandler<UpdateContactInfoCommand, bool>, UpdateContactsInfoHandler>();
        }
    }
}