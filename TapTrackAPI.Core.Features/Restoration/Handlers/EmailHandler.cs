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
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;
        public EmailHandler(DbContext dbContext, IMapper mapper, IMailSender mailSender, IConfiguration configuration) : base(dbContext, mapper)
        {
            _mailSender = mailSender;
            _configuration = configuration;
        }   

        public async Task<Unit> Handle(SendCodeQuery request, CancellationToken cancellationToken)
        {
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
