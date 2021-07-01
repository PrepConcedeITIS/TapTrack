using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    [UsedImplicitly]
    public class ProjectEditAsyncHandler : BaseHandler<ProjectEditCommand, ProjectDto>
    {
        private readonly IImageUploadService _imageUploadService;

        public ProjectEditAsyncHandler(DbContext dbContext, IMapper mapper, IImageUploadService imageUploadService)
            : base(dbContext, mapper)
        {
            _imageUploadService = imageUploadService;
        }

        public override async Task<ProjectDto> Handle(ProjectEditCommand input, CancellationToken cancellationToken)
        {
            var projects = DbContext.Set<Entities.Project>();

            var existingProject = await projects
                .FindAsync(input.ProjectId);
            var logoUrl = existingProject.LogoUrl;
            if (input.Logo != null)
            {
                logoUrl = await _imageUploadService.UploadProjectLogoImageAsync(input.Logo,
                    existingProject.CreatorId.ToString(),
                    input.IdVisible);
            }

            existingProject.UpdateGeneralInfo(input.Name, input.IdVisible, input.Description, logoUrl);

            var updated = projects.Update(existingProject);
            await DbContext.SaveChangesAsync(cancellationToken);
            var res = Mapper.Map<ProjectDto>(updated.Entity);
            return res;
        }
    }
}