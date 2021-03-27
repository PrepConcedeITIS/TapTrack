using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Project.Records;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Handlers
{
    public class UpdateProjectInfoAsyncHandler : IAsyncCommandHandler<ProjectEditCommand, ProjectDto>
    {
        private readonly DbContext _dbContext;
        private readonly IImageUploadService _imageUploadService;
        private readonly IMapper _mapper;

        public UpdateProjectInfoAsyncHandler(DbContext dbContext, IImageUploadService imageUploadService, IMapper mapper)
        {
            _dbContext = dbContext;
            _imageUploadService = imageUploadService;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(ProjectEditCommand input)
        {
            var existingProject = await _dbContext.Set<Entities.Project>()
                .FindAsync(input.Id);
            var logoUrl = existingProject.LogoUrl;
            if (input.Logo != null)
            {
                logoUrl = await _imageUploadService.UploadProjectLogoImageAsync(input.Logo,
                    existingProject.CreatorId.ToString(),
                    input.IdVisible);
            }

            existingProject.UpdateGeneralInfo(input.Name, input.IdVisible, input.Description, logoUrl);

            var updated = _dbContext.Set<Entities.Project>().Update(existingProject);
            await _dbContext.SaveChangesAsync();
            var res = _mapper.Map<ProjectDto>(updated.Entity);
            return res;
        }
    }
}