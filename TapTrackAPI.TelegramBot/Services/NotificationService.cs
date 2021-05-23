using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TapTrackAPI.Core.Base.Utility;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.TelegramBot.Enums;
using TapTrackAPI.TelegramBot.Interfaces;
using Telegram.Bot.Types.Enums;

namespace TapTrackAPI.TelegramBot.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IChatService _chatService;
        private readonly UserManager<User> _userManager;
        private readonly DbContext _dbContext;
        private readonly IHostEnvironment _environment;

        public NotificationService(IChatService chatService, DbContext dbContext, UserManager<User> userManager,
            IHostEnvironment environment)
        {
            _chatService = chatService;
            _dbContext = dbContext;
            _userManager = userManager;
            _environment = environment;
        }

        public async Task<TelegramNotificationStatus> SendIssueAssignmentNotification(ClaimsPrincipal actionAuthor,
            Issue issue, TeamMember assignee)
        {
            if (!_environment.IsProduction())
                return TelegramNotificationStatus.DeclinedBySystem;

            var actorId = _userManager.GetUserIdGuid(actionAuthor);
            if (actorId == assignee.UserId)
                return TelegramNotificationStatus.DeclinedBySystem;
            var tgConnection = await _dbContext.Set<TelegramConnection>()
                .FirstOrDefaultAsync(x => x.UserId == assignee.UserId);
            if (tgConnection is not {IsNotificationsEnabled: true})
                return TelegramNotificationStatus.UserNotificationsDisabled;

            var url = $"{(_environment.IsDevelopment() ? "localhost:4200" : "taptrack.tech")}/issue/{issue.Id}";
            var message = $"<p>New <a href=\"{url}\">issue</a> was assigned to you</p>" +
                          $"<p><b>{issue.Title}</b></p>" +
                          $"<p>Status: {issue.State}</p>" +
                          $"<p>Priority: {issue.Priority}</p>" +
                          $"<p>Estimate time: {TimeSpanFormatter.FormatterFromTimeSpan(issue.Estimation)}</p>" +
                          $"<p>Spent time: {TimeSpanFormatter.FormatterFromTimeSpan(issue.Spent)}</p>";

            var isDelivered = await _chatService.SendMessage(tgConnection.ChatId, message, ParseMode.Html);

            return isDelivered ? TelegramNotificationStatus.Success : TelegramNotificationStatus.Failed;
        }

        public async Task<TelegramNotificationStatus> SendIssueStatusChangeNotification(ClaimsPrincipal actionAuthor,
            State previousStatus, Issue issue)
        {
            if (!_environment.IsProduction())
                return TelegramNotificationStatus.DeclinedBySystem;

            var actorId = _userManager.GetUserIdGuid(actionAuthor);
            if (actorId == issue.Assignee.UserId)
                return TelegramNotificationStatus.DeclinedBySystem;

            var tgConnection = await _dbContext.Set<TelegramConnection>()
                .FirstOrDefaultAsync(x => x.UserId == issue.Assignee.UserId);
            if (tgConnection is not {IsNotificationsEnabled: true})
                return TelegramNotificationStatus.UserNotificationsDisabled;

            var url = $"{(_environment.IsDevelopment() ? "localhost:4200" : "taptrack.tech")}/issue/{issue.Id}";
            var message = $"[Issue]({url}) status changed: " +
                          $"*{previousStatus}* → *{issue.State}*";

            var isDelivered = await _chatService.SendMessage(tgConnection.ChatId, message);

            return isDelivered ? TelegramNotificationStatus.Success : TelegramNotificationStatus.Failed;
        }
    }
}