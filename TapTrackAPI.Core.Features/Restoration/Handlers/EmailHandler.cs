using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Restoration.DTO;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Restoration.Handlers
{
    public class EmailHandler : RequestHandlerBase, IRequestHandler<SendCodeQuery>
    {
        private IMailSender _mailSender;
        public IConfiguration Configuration;
        public EmailHandler(DbContext dbContext, IMapper _mapper, IMailSender mailSender, IConfiguration configuration) : base(dbContext, _mapper)
        {
            _mailSender = mailSender;
            Configuration = configuration;
        }   

        public async Task<Unit> Handle(SendCodeQuery request, CancellationToken cancellationToken)
        {
            var userMail = request.UserMail;
            var code = 123456;
            var curDate = DateTime.Now;
            var expDate = curDate.AddHours(1);
            var restoreEnt = new RestorationCode(userMail, curDate, expDate, code);
            var mailFrom = new MailAddress(Configuration.GetSection("Credentials").GetSection("Mail").Value);
            var mailTo = new MailAddress(userMail);
            var message = new MailMessage(mailFrom, mailTo);
            message.Subject = "Your restoration code on one hour";
            message.Body = code.ToString();
            await _mailSender.SendMessageAsync(message);
            Context.Add(restoreEnt);
            Context.SaveChanges();
            return default;
        }        
    }
}
