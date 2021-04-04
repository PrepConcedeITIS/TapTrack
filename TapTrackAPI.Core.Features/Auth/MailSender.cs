
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TapTrackAPI.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace TapTrackAPI.Core.Features.Auth
{
    public class MailSender : IMailSender
    {
        private readonly IConfiguration _config;

        public MailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendMessageAsync(MailAddress from, MailAddress to, MailMessage m)
        {           

            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new("smtp.gmail.com", 587);

            var credentials = _config.GetSection("Credentials")
                .GetChildren()
                .ToDictionary(x => x.Key, y => y.Value);
            smtp.Credentials = new NetworkCredential(credentials["Mail"], credentials["Password"]);
                

            smtp.EnableSsl = true;

            await smtp.SendMailAsync(m);
        }
        
    }
}
