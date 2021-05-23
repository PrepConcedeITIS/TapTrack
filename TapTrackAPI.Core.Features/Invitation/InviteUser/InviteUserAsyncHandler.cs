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
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Invitation.InviteUser
{
    [UsedImplicitly]
    public class InviteUserAsyncHandler : BaseCommandHandler, IRequestHandler<InviteUserCommand, InvitationDto>
    {
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public InviteUserAsyncHandler(IMailSender mailSender, DbContext dbContext, IMapper mapper,
            IConfiguration configuration, UserManager<User> userManager) : base(dbContext,
            mapper)
        {
            _mailSender = mailSender;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<InvitationDto> Handle(InviteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var project = await DbContext.Set<Entities.Project>()
                .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);
            
            var invite = new Entities.Invitation(user.Id, project.Id, InvitationState.Wait, request.Role);
            var entityEntry = await DbContext.AddAsync(invite, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            
            #warning //todo:change after deploy fixes
            var url = _configuration["Invitation:RemoteHostUrl"];
            var email = _configuration["Credentials:Mail"];
            var message = new MailMessage(new MailAddress(email, "TapTrack"),
                new MailAddress(request.Email))
            {
                Body =
                    $"Вы были приглашены в проект {project?.Name}, чтобы принять перейдите по ссылке: {url}?IsAccept=true&InvitationId={entityEntry.Entity.Id}" +
                    $"\nЧтобы отклонить перейдите по ссылке: {url}?IsAccept=false&InvitationId={entityEntry.Entity.Id}",
                IsBodyHtml = true
            };

            await _mailSender.SendMessageAsync(message);
            return Mapper.Map<InvitationDto>(invite);
        }
    }
}