using FluentValidation;
using Shelfie.Identity.BusinessLogic.UseCases.RegisterUser;
using Shelfie.Identity.BusinessLogic.Validators.Messages;

namespace Shelfie.Identity.BusinessLogic.Validators;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(ValidatorMessages.UsernameIsRequired);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ValidatorMessages.EmailIsRequired)
            .EmailAddress()
            .WithMessage(ValidatorMessages.EmailIsNotValid);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(ValidatorMessages.PasswordIsRequired);
    }
}
