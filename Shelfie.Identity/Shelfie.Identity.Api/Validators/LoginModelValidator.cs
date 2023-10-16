using FluentValidation;
using Shelfie.Identity.Api.Models;
using Shelfie.Identity.Api.Validators.Messages;

namespace Shelfie.Identity.Api.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(ValidatorMessages.UsernameIsRequired);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ValidatorMessages.PasswordIsRequired);
        }
    }
}
