using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TapTrackAPI.TelegramBot.Base;
using TapTrackAPI.TelegramBot.Interfaces;

namespace TapTrackAPI.TelegramBot
{
    public class Bot : IHostedService
    {
        private readonly IChatService _chatService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Bot> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public const string UnknownCommandMessage = "Unknown command. Try /help for a list of available commands.";

        public Bot(IChatService chatService, IServiceProvider serviceProvider, ILogger<Bot> logger, IHostEnvironment hostEnvironment)
        {
            _chatService = chatService;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_hostEnvironment.IsProduction())
            {
                _chatService.ChatMessage += OnChatMessage;
                _chatService.Callback += OnCallback;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_hostEnvironment.IsProduction())
            {
                _chatService.ChatMessage -= OnChatMessage;
            }

            return Task.CompletedTask;
        }

        private async void OnChatMessage(object? sender, ChatMessageEventArgs chatMessageArgs)
        {
            try
            {
                await ProcessChatMessage(sender, chatMessageArgs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Time}: OnChatMessage - Error {Exception}", DateTime.UtcNow, ex.Message);
            }
        }

        private async void OnCallback(object? sender, CallbackEventArgs callbackEventArgs)
        {
            try
            {
                await ProcessCallback(sender, callbackEventArgs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Time}: OnChatMessage - Error {Exception}", DateTime.UtcNow, ex.Message);
            }
        }

        private async Task ProcessChatMessage(object? sender, ChatMessageEventArgs chatMessageArgs)
        {
            if (sender is IChatService chatService)
            {
                var command = _serviceProvider.GetServices<IBotRequest>().SingleOrDefault(x =>
                    $"/{x.Command}".Equals(chatMessageArgs.Command, StringComparison.InvariantCultureIgnoreCase));
                using var serviceScope = _serviceProvider.CreateScope();
                if (command != null)
                {
                    await command.Execute(chatService,
                        serviceScope.ServiceProvider.GetRequiredService<DbContext>(),
                        chatMessageArgs.ChatId,
                        chatMessageArgs.UserId,
                        chatMessageArgs.MessageId,
                        chatMessageArgs.Text);
                }
                else
                {
                    _logger.LogTrace("Unknown command was sent");
                    // бот планируется быть сервисом уведомлений
                    //await chatService.SendMessage(chatMessageArgs.ChatId, UnknownCommandMessage);
                }
            }
        }

        private async Task ProcessCallback(object? sender, CallbackEventArgs callbackEventArgs)
        {
            if (sender is IChatService chatService)
            {
                var commandText = callbackEventArgs.Command?.Split(' ').First();
                var command = _serviceProvider.GetServices<IBotRequest>().SingleOrDefault(x =>
                    $"/{x.Command}".Equals(commandText, StringComparison.InvariantCultureIgnoreCase));
                using var serviceScope = _serviceProvider.CreateScope();

                if (command != null && !string.IsNullOrEmpty(commandText))
                {
                    await command.Execute(chatService, serviceScope.ServiceProvider.GetRequiredService<DbContext>(),
                        callbackEventArgs.ChatId,
                        callbackEventArgs.UserId,
                        callbackEventArgs.MessageId,
                        callbackEventArgs.Command?.Replace(commandText, string.Empty).Trim());
                }
                else
                {
                    _logger.LogCritical("Invalid callback data was provided: {CallbackData}", callbackEventArgs);
                }
            }
        }
    }
}