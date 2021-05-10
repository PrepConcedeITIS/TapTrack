using MediatR;

namespace TapTrackAPI.Core.Features.Restoration.VerifyCode
{
    public class VerifyCodeCommand : IRequest<Unit?>
    {
        public string UserMail { get; set; }
        public int UserCode { get; set; }
    }
}
