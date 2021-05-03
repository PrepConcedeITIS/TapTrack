using MediatR;

namespace TapTrackAPI.Core.Features.Restoration.SendCode
{
    public class SendCodeCommand : IRequest
    {
        public string UserMail { get; set; }
    }
}
