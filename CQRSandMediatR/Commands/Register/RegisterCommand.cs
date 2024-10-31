using MediatR;

namespace CQRSandMediatR.Commands.Register
{
    public class RegisterCommand: IRequest<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
