using MediatR;

namespace TapTrackAPI.Core.Features.Restoration.PasswordReset
{
    public class PasswordResetCommand : IRequest<Unit?>
    {
        public string UserMail { get; set; }
        public int UserCode { get; set; }
        public string UserPassword { get; set; }
    }
}