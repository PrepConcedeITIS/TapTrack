using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TapTrackAPI.TelegramBot.Commands;

namespace TapTrackAPI.TelegramBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBotCommands(this IServiceCollection services, Assembly assembly)
        {
            var typesToRegister = assembly
                .GetTypes()
                .Where(x => typeof(IBotCommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var typeToRegister in typesToRegister)
            {
                services.AddTransient(typeof(IBotCommand), typeToRegister);
            }

            return services;
        }
    }
}
