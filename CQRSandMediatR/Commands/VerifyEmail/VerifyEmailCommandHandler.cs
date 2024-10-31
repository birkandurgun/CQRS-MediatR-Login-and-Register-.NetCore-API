using CQRSandMediatR.Entities;
using CQRSandMediatR.Repositories;
using MediatR;

namespace CQRSandMediatR.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
    {
        private readonly IGenericRepository<User> _repository;

        public VerifyEmailCommandHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(u => u.EmailVerificationToken == request.Token);

            if (user == null)
                return false;

            if (user.IsEmailVerified)
                throw new Exception("Email is already verified");

            user.IsEmailVerified = true;
            await _repository.UpdateAsync(user);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
