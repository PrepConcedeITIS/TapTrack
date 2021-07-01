using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GravatarSharp.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Create
{
    [UsedImplicitly]
    public class CreateProjectAsyncHandler : BaseHandlerWithUserManager<ProjectCreateCommand, ProjectDto>
    {
        private readonly IImageUploadService _imageUpload;

        public CreateProjectAsyncHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager,
            IImageUploadService imageUpload) : base(dbContext, mapper, userManager)
        {
            _imageUpload = imageUpload;
        }

        public override async Task<ProjectDto> Handle(ProjectCreateCommand command, CancellationToken cancellationToken)
        {
            var creatorId = UserManager.GetUserIdGuid(command.ClaimsPrincipal);
            string link = null;
            if (command.Logo != null)
                link = await _imageUpload.UploadProjectLogoImageAsync(command.Logo, creatorId.ToString(),
                    command.IdVisible);
            else
                link = GravatarController.GetImageUrl($"{command.Name}@taptrack.tech", 256);
            var project = new Entities.Project(command.Name, command.IdVisible, command.Description, link, creatorId);
            var entityEntry = await DbContext.Set<Entities.Project>().AddAsync(project, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);

            var teamMember = new TeamMember(creatorId, entityEntry.Entity.Id, Role.Admin);
            await DbContext.AddAsync(teamMember, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<ProjectDto>(entityEntry.Entity);
        }
    }
}