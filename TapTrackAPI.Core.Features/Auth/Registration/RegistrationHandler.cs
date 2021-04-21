using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Auth.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Auth.Registration
{
    [UsedImplicitly]
    public class RegistrationHandler : IRequestHandler<RegistrationCommand, RegistrationResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly DbContext _dbContext;

        public RegistrationHandler(UserManager<User> userManager, IMapper mapper, IJwtTokenGenerator tokenGenerator, DbContext dbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _dbContext = dbContext;
        }

        public async Task<RegistrationResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var input = request.UserInput;
            var user = new User(input.Email);
            var result = await _userManager.CreateAsync(user, input.Password);
            var userDto = _mapper.Map<AuthUserDto>(user);
            
            var guidUserId = user.Id;

            var contactTypes = _dbContext.Set<ContactType>().AsQueryable();

            await _dbContext.Set<UserContact>().AddRangeAsync(
                new UserContact(guidUserId, String.Empty, false,
                    contactTypes.First(x => x.Name == ContactTypeConstants.TelegramName).Id),
                new UserContact(guidUserId, String.Empty, false, 
                    contactTypes.First(x => x.Name == ContactTypeConstants.DiscordName).Id),
                new UserContact(guidUserId, String.Empty, false, 
                    contactTypes.First(x => x.Name == ContactTypeConstants.SkypeName).Id),
                new UserContact(guidUserId, String.Empty, false, 
                    contactTypes.First(x => x.Name == ContactTypeConstants.GitHubName).Id));

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return new RegistrationResponse(userDto, _tokenGenerator.GenerateToken(user), result.Errors.ToArray(), input);
        }
    }
}