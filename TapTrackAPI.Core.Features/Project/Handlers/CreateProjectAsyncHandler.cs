using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.Project.Records;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Handlers
{
    public class CreateProjectAsyncHandler : IAsyncCommandHandler<ProjectCreateCommand, ProjectDto>, IRequestHandler<ProjectCreateCommand, ProjectDto>
    {
        private readonly IImageUploadService _imageUpload;
        private readonly UserManager<User> _userManager;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateProjectAsyncHandler(IImageUploadService imageUpload, UserManager<User> userManager,
            DbContext dbContext, IMapper mapper)
        {
            _imageUpload = imageUpload;
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(ProjectCreateCommand command)
        {
            var creatorId = _userManager.GetUserIdGuid(command.Claims);
            var link = await _imageUpload.UploadProjectLogoImageAsync(command.Logo, creatorId.ToString(),
                command.IdVisible);
            var project = new Entities.Project(command.Name, command.IdVisible, command.Description, link, creatorId);
            var entityEntry = await _dbContext.Set<Entities.Project>().AddAsync(project);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(entityEntry.Entity);
        }

        public Task<ProjectDto> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}