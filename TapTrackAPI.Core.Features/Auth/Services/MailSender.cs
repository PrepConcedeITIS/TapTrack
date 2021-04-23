﻿using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Auth.Services
{
    public class MailSender : IMailSender
    {
        private readonly IConfiguration _config;

        public MailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageAsync(MailMessage m)
        {
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