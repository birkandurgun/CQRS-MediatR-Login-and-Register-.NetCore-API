using MediatR;

namespace CQRSandMediatR.Commands.SendEmail
{
    public class SendVerificationEmailCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
