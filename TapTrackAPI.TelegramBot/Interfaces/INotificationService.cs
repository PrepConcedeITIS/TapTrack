using System.Security.Claims;
using System.Threading.Tasks;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.TelegramBot.Enums;

namespace TapTrackAPI.TelegramBot.Interfaces
{
    public interface INotificationService
    {
        Task<TelegramNotificationStatus> SendIssueAssignmentNotification(ClaimsPrincipal actionAuthor, Issue issue,
            TeamMember assignee);

        Task<TelegramNotificationStatus> SendIssueStatusChangeNotification(ClaimsPrincipal actionAuthor,
            State previousStatus, Issue issue);
    }
}