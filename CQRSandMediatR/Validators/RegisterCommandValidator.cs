using CQRSandMediatR.Commands.Register;
using FluentValidation;

namespace CQRSandMediatR.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() { 
            RuleFor(u => u.Firstname)
                .NotNull()
                .NotEmpty().WithMessage("Do not leave Firstname blank.");

            RuleFor(u => u.Lastname).NotNull().NotEmpty().WithMessage("Do not leave Lastname blank.");

            RuleFor(u => u.Email).NotNull().NotEmpty().WithMessage("Do not leave Email blank.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Invalid email");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Do not leave Password blank.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(u => u.ConfirmPassword).Equal(u => u.Password).WithMessage("Passwords must match");
        }
    }
}
