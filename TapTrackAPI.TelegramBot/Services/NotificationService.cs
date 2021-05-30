using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TapTrackAPI.Core.Base.Utility;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.TelegramBot.Enums;
using TapTrackAPI.TelegramBot.Interfaces;

namespace TapTrackAPI.TelegramBot.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IChatService _chatService;
        private readonly UserManager<User> _userManager;
        private readonly DbContext _dbContext;
        private readonly IHostEnvironment _environment;
        private readonly bool _tgEnabled;

        public NotificationService(IChatService chatService, DbContext dbContext, UserManager<User> userManager,
            IHostEnvironment environment)
        {
            _chatService = chatService;
            _dbContext = dbContext;
            _userManager = userManager;
            _environment = environment;
            _tgEnabled = bool.Parse(Environment.GetEnvironmentVariable(ConfigurationConstants.TelegramNotificationsEnabled)!); 
        }

        public async Task<TelegramNotificationStatus> SendIssueAssignmentNotification(ClaimsPrincipal actionAuthor,
            Issue issue, TeamMember? assignee)
        {
            Console.WriteLine(_tgEnabled);
            if (!_tgEnabled)
                return TelegramNotificationStatus.DeclinedBySystem;

            var actorId = _userManager.GetUserIdGuid(actionAuthor);
            if (assignee == null || actorId == assignee.UserId)
                return TelegramNotificationStatus.DeclinedBySystem;
            var tgConnection = await _dbContext.Set<TelegramConnection>()
                .FirstOrDefaultAsync(x => x.UserId == assignee.UserId);
            if (tgConnection is not {IsNotificationsEnabled: true})
                return TelegramNotificationStatus.UserNotificationsDisabled;

            var url = $"{(_environment.IsDevelopment() ? "localhost:4200" : "www.taptrack.tech")}/issue/{issue.Id}";
            var message = $"New [issue]({url}) was assigned to you\n" +
                          $"*{issue.Title}*\n" +
                          $"Status: {issue.State}\n" +
                          $"Priority: {issue.Priority}\n" +
                          $"Estimate time: {TimeSpanFormatter.FormatterFromTimeSpan(issue.Estimation)}\n" +
                          $"Spent time: {TimeSpanFormatter.FormatterFromTimeSpan(issue.Spent)}\n";

            var isDelivered = await _chatService.SendMessage(tgConnection.ChatId, message);

            return isDelivered ? TelegramNotificationStatus.Success : TelegramNotificationStatus.Failed;
        }

        public async Task<TelegramNotificationStatus> SendIssueStatusChangeNotification(ClaimsPrincipal actionAuthor,
            State previousStatus, Issue issue)
        {
            if (!_tgEnabled)
                return TelegramNotificationStatus.DeclinedBySystem;

            if (!((previousStatus == State.Review && issue.State == State.Incomplete) ||
                (previousStatus == State.InTest && issue.State == State.Incomplete) ||
                (previousStatus == State.Acceptance && issue.State == State.Incomplete)))
                return TelegramNotificationStatus.DeclinedBySystem;
            
            var actorId = _userManager.GetUserIdGuid(actionAuthor);
            if (issue.Assignee == null || actorId == issue.Assignee.UserId)
                return TelegramNotificationStatus.DeclinedBySystem;

            var tgConnection = await _dbContext.Set<TelegramConnection>()
                .FirstOrDefaultAsync(x => x.UserId == issue.Assignee.UserId);
            if (tgConnection is not {IsNotificationsEnabled: true})
                return TelegramNotificationStatus.UserNotificationsDisabled;

            var url = $"{(_environment.IsDevelopment() ? "localhost:4200" : "www.taptrack.tech")}/issue/{issue.Id}";
            var message = $"[Issue]({url}) status changed: " +
                          $"*{previousStatus}* → *{issue.State}*";

            var isDelivered = await _chatService.SendMessage(tgConnection.ChatId, message);

            return isDelivered ? TelegramNotificationStatus.Success : TelegramNotificationStatus.Failed;
        }
    }
}