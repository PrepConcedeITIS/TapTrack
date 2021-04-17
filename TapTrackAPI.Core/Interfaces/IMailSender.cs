using System.Net.Mail;
using System.Threading.Tasks;

namespace TapTrackAPI.Core.Interfaces
{
    public interface IMailSender
    {
        public Task SendMessageAsync(MailMessage message);
    }
}
