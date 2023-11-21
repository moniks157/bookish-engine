using FluentValidation;
using Shelfie.Identity.BusinessLogic.UseCases.LoginUser;
using Shelfie.Identity.BusinessLogic.Validators.Messages;

namespace Shelfie.Identity.BusinessLogic.Validators;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator() 
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(ValidatorMessages.UsernameIsRequired);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(ValidatorMessages.PasswordIsRequired);
    }
}
