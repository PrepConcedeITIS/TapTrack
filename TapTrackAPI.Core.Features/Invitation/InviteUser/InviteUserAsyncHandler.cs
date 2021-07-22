using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Invitation.Dto;

namespace TapTrackAPI.Core.Features.Invitation.InviteUser
{
    [UsedImplicitly]
    public class InviteUserAsyncHandler : BaseHandler<InviteUserCommand, InvitationGridDto>
    {
        private readonly UserManager<User> _userManager;

        public InviteUserAsyncHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper)
        {
            _userManager = userManager;
        }

        public override async Task<InvitationGridDto> Handle(InviteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var project = await DbContext
                .Set<Entities.Project>()
                .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);

            var invite = new Entities.Invitation(user.Id, project.Id, InvitationState.Wait, request.Role);
            var entityEntry = await DbContext.AddAsync(invite, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<InvitationGridDto>(entityEntry.Entity);
        }
    }
}