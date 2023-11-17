using FluentValidation;

namespace Application.Features.User.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Field must be an email address");
        
        RuleFor(x => x.UserName)
            .MinimumLength(5)
            .WithMessage("Minimum userName Length is 5");

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmedPassword)
            .WithMessage("Password must be equal to confirmation password");
    }
}