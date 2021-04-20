using MediatR;
using TapTrackAPI.Core.Features.Auth.Base;

namespace TapTrackAPI.Core.Features.Auth.Registration
{
    public record RegistrationCommand(UserInput UserInput) : IRequest<RegistrationResponse>, IHasUserInput;
}