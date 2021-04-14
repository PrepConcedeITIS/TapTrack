using MediatR;

namespace TapTrackAPI.Core.Features.Restoration.DTO
{
    public class NewPasswordQuery : IRequest
    {
        public string UserMail { get; set; }
        public string Password { get; set; }
    }
}