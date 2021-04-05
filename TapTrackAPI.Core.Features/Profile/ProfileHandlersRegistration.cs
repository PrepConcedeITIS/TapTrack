using System.Collections.Generic;
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
            services.AddScoped<IAsyncQueryHandler<GetUserProfileQuery, UserProfileDto>, GetUserProfileHandler>();
            services.AddScoped<IAsyncQueryHandler<GetUserProjectsQuery, List<UserProjectDto>>, GetUserProjectsHandler>();
            services.AddScoped<IAsyncQueryHandler<ChangeUserNameCommand, UserProfileDto>, ChangeUserNameHandler>();
            services.AddScoped<IAsyncQueryHandler<UpdateProfileImageCommand, UserProfileDto>, UpdateProfileImageHandler>();
            services.AddScoped<IAsyncQueryHandler<GetContactInfoQuery, List<ContactInformationDto>>,
                    GetContactInfoHandler>();
            services.AddScoped<IAsyncQueryHandler<UpdateContactInfoCommand, List<ContactInformationDto>>, UpdateContactsInfoHandler>();
        }
    }
}