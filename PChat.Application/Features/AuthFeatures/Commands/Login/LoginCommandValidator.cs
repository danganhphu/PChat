using FluentValidation;
using Microsoft.Extensions.Localization;

namespace PChat.Application.Features.AuthFeatures.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(IStringLocalizer<LoginCommandValidator> localizer)
    {
        RuleFor(p => p.UserNameOrEmail)
            .NotEmpty().WithMessage(localizer["UsernameEmailNotNull"])
            .NotNull().WithMessage(localizer["UsernameEmailNotEmpty"])
            .MinimumLength(3).WithMessage(localizer["UsernameEmailMinLength"])
            .EmailAddress().WithMessage(localizer["InvalidEmail"])
            .MaximumLength(255).WithMessage(localizer["EmailMaxLength"]);
        
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
