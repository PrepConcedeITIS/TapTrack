using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TapTrackAPI.Core.Interfaces
{
    public interface IMailSender
    {
        public Task SendMessageAsync(MailAddress mailFrom, MailAddress mailTo, MailMessage message);
    }
}
