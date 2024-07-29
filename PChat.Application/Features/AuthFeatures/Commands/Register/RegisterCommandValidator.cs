using FluentValidation;
using Microsoft.Extensions.Localization;

namespace PChat.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IStringLocalizer<RegisterCommandValidator> localizer)
    {
        RuleFor(p => p.Email)
            .NotNull().WithMessage(localizer["UsernameNotNull"])
            .NotEmpty().WithMessage(localizer["UsernameNotEmpty"])
            .EmailAddress().WithMessage(localizer["InvalidEmail"]);
            
        RuleFor(p => p.UserName)
            .NotNull().WithMessage(localizer["EmailNotNull"])
            .NotEmpty().WithMessage(localizer["EmailNotEmpty"])
            .MinimumLength(3).WithMessage(localizer["UsernameMinLength"]);
        
        RuleFor(p => p.Password)
            .NotNull().WithMessage(localizer["PasswordNotNull"])
            .NotEmpty().WithMessage(localizer["PasswordNotEmpty"])
            .Matches("[A-Z]").WithMessage(localizer["PasswordUppercase"])
            .Matches("[a-z]").WithMessage(localizer["PasswordLowercase"])
            .Matches("[0-9]").WithMessage(localizer["PasswordDigit"])
            .Matches("[^a-zA-Z0-9]").WithMessage(localizer["PasswordSpecialCharacter"])
            .MinimumLength(3).WithMessage(localizer["PasswordMinLength"])
            .Matches(@"^[^\s]+$").WithMessage(localizer["PasswordNoSpaces"])
            .MaximumLength(50).WithMessage(localizer["PasswordMaxLength"]);
    }

}
