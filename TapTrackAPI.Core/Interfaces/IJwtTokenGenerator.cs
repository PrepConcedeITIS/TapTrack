using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}