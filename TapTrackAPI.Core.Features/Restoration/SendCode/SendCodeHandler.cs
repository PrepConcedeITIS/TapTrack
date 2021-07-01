using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Restoration.SendCode
{
    [UsedImplicitly]
    public class SendCodeHandler : RequestHandlerBase, IRequestHandler<SendCodeCommand>
    {
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public SendCodeHandler(DbContext dbContext, UserManager<User> userManager, IMapper mapper, IMailSender mailSender,
            IConfiguration configuration) : base(dbContext, mapper)
        {
            _userManager = userManager;
            _mailSender = mailSender;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(SendCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserMail);
            if (user == null) return default;
            var userMail = request.UserMail;
            var rnd = new Random();
            var code = rnd.Next(100000, 999999);
            var curDate = DateTime.Now;
            var expDate = curDate.AddHours(1);
            var restoreEnt = new RestorationCode(userMail, curDate, expDate, code);
            var mailFrom = new MailAddress(_configuration.GetSection("Credentials").GetSection("Mail").Value);
            var mailTo = new MailAddress(userMail);
            var message = new MailMessage(mailFrom, mailTo)
            {
                Subject = "Your restoration code on one hour", Body = code.ToString()
            };
            await _mailSender.SendMessageAsync(message);
            Context.Add(restoreEnt);
            await Context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}