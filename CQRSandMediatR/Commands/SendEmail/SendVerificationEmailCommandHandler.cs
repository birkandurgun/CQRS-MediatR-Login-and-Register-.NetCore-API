using CQRSandMediatR.Entities;
using CQRSandMediatR.Repositories;
using MediatR;
using System.Net;
using System.Net.Mail;

namespace CQRSandMediatR.Commands.SendEmail
{
    public class SendVerificationEmailCommandHandler : IRequestHandler<SendVerificationEmailCommand, bool>
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<User> _repository;

        public SendVerificationEmailCommandHandler(IConfiguration configuration, IGenericRepository<User> repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<bool> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(u => u.Email == request.Email);

            if (user.IsEmailVerified)
                throw new Exception("Email is already verified");

            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:From"]),
                Subject = "Please verify your email",
                Body = $"Your Verification Code: {user.EmailVerificationToken}",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(request.Email);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
