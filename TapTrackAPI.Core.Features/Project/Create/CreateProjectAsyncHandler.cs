using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Create
{
    public class CreateProjectAsyncHandler : IRequestHandler<ProjectCreateCommand, ProjectDto>
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

        public async Task<ProjectDto> Handle(ProjectCreateCommand command, CancellationToken cancellationToken)
        {
            var creatorId = _userManager.GetUserIdGuid(command.ClaimsPrincipal);
            var link = await _imageUpload.UploadProjectLogoImageAsync(command.Logo, creatorId.ToString(),
                command.IdVisible);
            var project = new Entities.Project(command.Name, command.IdVisible, command.Description, link, creatorId);
            var entityEntry = await _dbContext.Set<Entities.Project>().AddAsync(project, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProjectDto>(entityEntry.Entity);
        }
    }
}