using MediatR;
using TapTrackAPI.Core.Features.Auth.Base;

namespace TapTrackAPI.Core.Features.Auth.Login
{
    public record LoginCommand(UserInput UserInput): IRequest<LoginResponse>, IHasUserInput;
}