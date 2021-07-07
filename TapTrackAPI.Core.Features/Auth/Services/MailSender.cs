using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TapTrackAPI.Core.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TapTrackAPI.Core.Features.Auth.Services
{
    public class MailSender : IMailSender
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public MailSender(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendMessageAsync(MailMessage m)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var authToken = Encoding.ASCII.GetBytes($"api:1b2b8e4cb2126148211bbe6eed4f707f-c4d287b4-40a91ea3");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"from", $"sandboxf4299dcf9c864470b8df5cdc863d7b24.mailgun.org <brad@sandboxf4299dcf9c864470b8df5cdc863d7b24.mailgun.org>"},
                {"h:Reply-To", $"sandboxf4299dcf9c864470b8df5cdc863d7b24.mailgun.org  <brad@sandboxf4299dcf9c864470b8df5cdc863d7b24.mailgun.org>"},
                {"to", "t1g2r3mr@gmail.com"},
                {"subject", "mail12"},
                {"text", "txtmsg"},
                {"html", "htmlmsg"}
            });
            var result =
                await httpClient.PostAsync($"https://api.mailgun.net/v3/sandboxf4299dcf9c864470b8df5cdc863d7b24.mailgun.org/messages",
                    formContent);
            result.EnsureSuccessStatusCode();
            /*
            SmtpClient smtp = new("smtp.mailgun.org", 587);

            var credentials = _configuration.GetSection("SMTP");
            
            smtp.Credentials = new NetworkCredential(credentials["Mail"], credentials["Password"]);

            smtp.EnableSsl = true;

            await smtp.SendMailAsync(m);
            */
        }
    }
}