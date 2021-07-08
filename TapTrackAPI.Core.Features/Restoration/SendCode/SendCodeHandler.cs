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
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Restoration.SendCode
{
    [UsedImplicitly]
    public class SendCodeHandler : BaseHandlerWithUserManager<SendCodeCommand>
    {
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;
        private readonly Random _random;

        public SendCodeHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager,
            IConfiguration configuration, IMailSender mailSender)
            : base(dbContext, mapper, userManager)
        {
            _configuration = configuration;
            _mailSender = mailSender;
            _random = new Random();
        }

        public override async Task<Unit> Handle(SendCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await UserManager.FindByEmailAsync(request.UserMail);
            if (user == null)
                return default;
            var userMail = request.UserMail;
            var code = _random.Next(100000, 999999);
            var curDate = DateTime.Now;
            var expDate = curDate.AddHours(1);
            var restoreEnt = new RestorationCode(userMail, curDate, expDate, code);
            var mailTo = new MailAddress(userMail);
            var message = new MailMessage()
            {
                To = {mailTo},
                Subject = "Your restoration code on one hour", Body = code.ToString()
            };
            await _mailSender.SendMessageAsync(message);
            DbContext.Add(restoreEnt);
            await DbContext.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}