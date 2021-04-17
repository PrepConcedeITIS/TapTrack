using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    [UsedImplicitly]
    public class ProjectEditAsyncHandler : IRequestHandler<ProjectEditCommand, ProjectDto>
    {
        private readonly DbContext _dbContext;
        private readonly IImageUploadService _imageUploadService;
        private readonly IMapper _mapper;

        public ProjectEditAsyncHandler(DbContext dbContext, IImageUploadService imageUploadService,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _imageUploadService = imageUploadService;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(ProjectEditCommand input, CancellationToken cancellationToken)
        {
            var projects = _dbContext.Set<Entities.Project>();

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
            await _dbContext.SaveChangesAsync(cancellationToken);
            var res = _mapper.Map<ProjectDto>(updated.Entity);
            return res;
        }
    }
}