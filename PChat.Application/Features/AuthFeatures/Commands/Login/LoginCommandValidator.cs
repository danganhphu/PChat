using FluentValidation;
using Microsoft.Extensions.Localization;

namespace PChat.Application.Features.AuthFeatures.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(p => p.UserNameOrEmail).NotEmpty().WithMessage("Username or email cannot be empty!");
        RuleFor(p => p.UserNameOrEmail).NotNull().WithMessage("Username or email cannot be empty!");
        RuleFor(p => p.UserNameOrEmail).MinimumLength(3).WithMessage("Username or email must be at least 3 characters long!");
        RuleFor(p => p.Password).NotEmpty().WithMessage("Password cannot be empty!");
        RuleFor(p => p.Password).NotNull().WithMessage("Password cannot be empty!");
        RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter!");
        RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter!");
        RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Password must contain at least 1 digit!");
        RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 special character!");
    }
}
