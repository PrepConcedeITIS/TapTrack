namespace TapTrackAPI.Core.Features.Auth.Base
{
    public interface IHasUserInput
    {
        public UserInput UserInput { get; init; }
    }
}