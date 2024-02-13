using FluentValidation;
using Store.Auth.Common.DTO;

namespace Store.WebApi.Common.Validation.AuthController;

public class SignInCredentialsValidator : AbstractValidator<SignInCredentials>
{
    public SignInCredentialsValidator()
    {
        RuleFor(x => x.Username)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Username is required")
            .NotEmpty().WithMessage("Username cannot be empty")
            .MaximumLength(255).WithMessage("Username length cannot exceed 255 characters")
            .MinimumLength(6).WithMessage("Username length cannot be less than 6 characters");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Password is required")
            .NotEmpty().WithMessage("Password cannot be empty")
            .MaximumLength(255).WithMessage("Password length cannot exceed 255 characters")
            .MinimumLength(6).WithMessage("Password length cannot be less than 6 characters");
    }
}