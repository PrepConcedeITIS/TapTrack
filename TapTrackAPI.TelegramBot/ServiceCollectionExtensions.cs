using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TapTrackAPI.TelegramBot.Commands;

namespace TapTrackAPI.TelegramBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBotCommands(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient(typeof(IBotRequest), typeof(StartRequest));
            //var typesToRegister = assembly
            //    .GetTypes()
            //    .Where(x => typeof(IBotCommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
//
            //foreach (var typeToRegister in typesToRegister)
            //{
            //    services.AddScoped(typeof(IBotCommand), typeToRegister);
            //}

            return services;
        }
    }
}
