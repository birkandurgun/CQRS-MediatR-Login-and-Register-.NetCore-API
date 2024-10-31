using MediatR;

namespace CQRSandMediatR.Commands.VerifyEmail
{
    public class VerifyEmailCommand: IRequest<bool>
    {
        public string Token { get; set; }
    }
}
