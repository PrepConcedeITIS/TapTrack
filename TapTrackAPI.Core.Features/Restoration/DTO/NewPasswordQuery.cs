using MediatR;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Restoration.DTO
{
    public class NewPasswordQuery : IRequest<RestorationCode>
    {
        public string UserMail { get; set; }
        public int UserCode { get; set; }
        
        public string UserPassword { get; set; }
        
    }
}