using FluentValidation;
using Shelfie.Identity.Api.Models;
using Shelfie.Identity.Api.Validators.Messages;

namespace Shelfie.Identity.Api.Validators
{
    public class RegisterModeValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModeValidator()
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
}
