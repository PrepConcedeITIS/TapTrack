using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class InviteUserAsyncHandler : BaseCommandHandler, IRequestHandler<InviteUserCommand, InvitationDto>
    {
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;

        public InviteUserAsyncHandler(IMailSender mailSender, DbContext dbContext, IMapper mapper,
            IConfiguration configuration) : base(dbContext,
            mapper)
        {
            _mailSender = mailSender;
            _configuration = configuration;
        }

        public async Task<InvitationDto> Handle(InviteUserCommand request, CancellationToken cancellationToken)
        {
            var user = DbContext.Set<User>().FirstOrDefault(x => x.Email == request.Email);
            var project = DbContext.Set<Entities.Project>().FirstOrDefault(x => x.Id == request.ProjectId);
            var invite = new Entities.Invitation(user, project, InvitationState.Wait, request.Role);
            DbContext.Add(invite);
            await DbContext.SaveChangesAsync(cancellationToken);
            var url = _configuration.GetSection("Invitation").GetSection("URL").Value;
            var email = _configuration.GetSection("Credentials").GetSection("Mail").Value;
            var message = new MailMessage(new MailAddress(email, "ТапТрек"),
                new MailAddress(request.Email))
            {
                Body =
                    $"Вы были приглашены в проект {project?.Name}, чтобы принять перейдите по ссылке: ${url}?InvitationId={invite.Id}&IsAccept=true" +
                    $"\nЧтобы отклонить перейдите по ссылке: ${url}?InvitationId={invite.Id}&IsAccept=false"
            };

            await _mailSender.SendMessageAsync(message);
            return Mapper.Map<InvitationDto>(invite);
        }
    }
}