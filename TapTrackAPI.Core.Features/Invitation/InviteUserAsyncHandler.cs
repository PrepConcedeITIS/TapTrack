using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class InviteUserAsyncHandler : BaseCommandHandler, IRequestHandler<InviteUserCommand, InvitationDto>
    {
        private readonly IMailSender _mailSender;

        public InviteUserAsyncHandler(IMailSender mailSender, DbContext dbContext, IMapper mapper) : base(dbContext,
            mapper)
        {
            _mailSender = mailSender;
        }

        public async Task<InvitationDto> Handle(InviteUserCommand request, CancellationToken cancellationToken)
        {
            var user = DbContext.Set<User>().FirstOrDefault(x => x.Email == request.Email);
            var project = DbContext.Set<Entities.Project>().FirstOrDefault(x => x.Id == request.ProjectId);
            var invite = new Entities.Invitation(user, project, InvitationState.Wait, request.Role);
            DbContext.Add(invite);
            DbContext.SaveChanges();

            var message = new MailMessage(new MailAddress("taptrack@noreply.com", "ТапТрек"),
                new MailAddress(request.Email))
            {
                Body =
                    $"Вы были приглашены в проект {project?.Name}, чтобы принять перейдите по ссылке: https://localhost:5001/api/Invitation/AcceptOrDeclineInvitation?InvitationId={invite.Id}&IsAccept=true" +
                    $"\nЧтобы отклонить перейдите по ссылке: https://localhost:5001/api/Invitation/AcceptOrDeclineInvitation?InvitationId={invite.Id}&IsAccept=false"
            };

            await _mailSender.SendMessageAsync(message);
            return Mapper.Map<InvitationDto>(invite);
        }
    }
}