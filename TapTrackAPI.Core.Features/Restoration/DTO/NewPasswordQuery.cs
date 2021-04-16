using MediatR;

namespace TapTrackAPI.Core.Features.Restoration.DTO
{
    public class NewPasswordQuery : IRequest
    {
        public string UserMail { get; set; }
        public string UserPassword { get; set; }
        
        public string UserCode { get; set; }
    }
}