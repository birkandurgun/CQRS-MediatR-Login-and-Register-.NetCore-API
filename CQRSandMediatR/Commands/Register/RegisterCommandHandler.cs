using CQRSandMediatR.Entities;
using CQRSandMediatR.PasswordHash;
using CQRSandMediatR.Repositories;
using CQRSandMediatR.Validators;
using FluentValidation.Results;
using MediatR;

namespace CQRSandMediatR.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IGenericRepository<User> _repository;
        private readonly int _iteration = 5;
        private readonly string _pepper;

        public RegisterCommandHandler(IGenericRepository<User> repository,IConfiguration configuration)
        {
            _repository = repository;
            _pepper = configuration["SecuritySettings:Pepper"];
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            RegisterCommandValidator validator = new RegisterCommandValidator();

            ValidationResult results = validator.Validate(request);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    var errorMessages = string.Join(Environment.NewLine, results.Errors.Select(f => f.ErrorMessage));
                    throw new Exception(errorMessages);
                }
            }

            var existingUser = await _repository.GetAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            string salt = PasswordHasher.CreateSalt();
            string hashedPassword = PasswordHasher.Hash(request.Password, salt, _pepper, _iteration);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                IsEmailVerified = false,
                EmailVerificationToken = Guid.NewGuid().ToString()
            };

            await _repository.AddAsync(newUser);
            await _repository.SaveChangesAsync();

            return newUser.Id;
        }
    }
}
