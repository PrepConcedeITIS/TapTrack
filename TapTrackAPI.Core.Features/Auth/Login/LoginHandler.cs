using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Auth.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Auth.Login
{
    [UsedImplicitly]
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;

        public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager,
            IJwtTokenGenerator tokenGenerator, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var (email, password) = request.UserInput;
            var user = await _userManager.FindByEmailAsync(email);
            var signInResult =
                await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!signInResult.Succeeded)
                return null;

            var userDto = _mapper.Map<AuthUserDto>(user);
            return new LoginResponse(userDto, _tokenGenerator.GenerateToken(user));
        }
    }
}