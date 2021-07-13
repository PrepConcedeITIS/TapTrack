using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Invitation.InviteUser
{
    [UsedImplicitly]
    public class InviteUserAsyncHandler : BaseHandler<InviteUserCommand, InvitationDto>
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

        public override async Task<InvitationDto> Handle(InviteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var project = await DbContext.Set<Entities.Project>()
                .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);
            
            var invite = new Entities.Invitation(user.Id, project.Id, InvitationState.Wait, request.Role);
            var entityEntry = await DbContext.AddAsync(invite, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            
            var url = _configuration["Invitation:RemoteHostUrl"];
            var message = new MailMessage
            {
                To = { new MailAddress(request.Email)},
                Subject = "TapTrack project invitation",
                Body =
                    $"You were invited to {project?.Name} project." +
                    $"\nTo accept invite follow this link: {url}?IsAccept=true&InvitationId={entityEntry.Entity.Id}" +
                    $"\nTo decline invite follow this link: {url}?IsAccept=false&InvitationId={entityEntry.Entity.Id}",
                IsBodyHtml = true
            };
            await _mailSender.SendMessageAsync(message);
            return Mapper.Map<InvitationDto>(invite);
        }
    }
}