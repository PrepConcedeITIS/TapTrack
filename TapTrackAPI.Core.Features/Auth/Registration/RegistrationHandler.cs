using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

        public RegistrationHandler(UserManager<User> userManager, IMapper mapper, IJwtTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<RegistrationResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var input = request.UserInput;
            var user = new User(input.Email);
            var result = await _userManager.CreateAsync(user, input.Password);
            var userDto = _mapper.Map<AuthUserDto>(user);
            return new RegistrationResponse(userDto, _tokenGenerator.GenerateToken(user), result.Errors.ToArray(), input);
        }
    }
}