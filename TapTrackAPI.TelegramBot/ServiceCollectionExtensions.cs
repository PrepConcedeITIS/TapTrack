using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TapTrackAPI.TelegramBot.Base;
using TapTrackAPI.TelegramBot.Commands.Start;

namespace TapTrackAPI.TelegramBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBotCommands(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient(typeof(IBotRequest), typeof(StartRequest));

            return services;
        }
    }
}
