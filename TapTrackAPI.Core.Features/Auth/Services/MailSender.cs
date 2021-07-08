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
            var smtpSection = _configuration.GetSection("SMTP");
            var emailSource = smtpSection["EmailSource"];
            var authToken = Encoding.ASCII.GetBytes($"api:{smtpSection["MailGunApiKey"]}");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"from", $"{emailSource} <{emailSource}>"},
                {"h:Reply-To", $"{emailSource}  <{emailSource}>"},
                {"to", $"{m.To.First().Address}"},
                {"subject", $"{m.Subject}"},
                {"html", $"{m.Body}"}
            });
            var result =
                await httpClient.PostAsync($"https://api.mailgun.net/v3/{smtpSection["Domain"]}/messages",
                    formContent);
            result.EnsureSuccessStatusCode();
        }
    }
}