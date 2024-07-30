using FluentValidation;
using Microsoft.Extensions.Localization;

namespace PChat.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(p => p.Email).NotEmpty().WithMessage("Email cannot be empty!");
        RuleFor(p => p.Email).NotNull().WithMessage("Email cannot be empty!");
        RuleFor(p => p.Email).EmailAddress().WithMessage("Please enter a valid email address!");
        RuleFor(p => p.UserName).NotEmpty().WithMessage("Username cannot be empty!");
        RuleFor(p => p.UserName).NotNull().WithMessage("Username cannot be empty!");
        RuleFor(p => p.UserName).MinimumLength(3).WithMessage("Username must be at least 3 characters long!");
        RuleFor(p => p.Password).NotEmpty().WithMessage("Password cannot be empty!");
        RuleFor(p => p.Password).NotNull().WithMessage("Password cannot be empty!");
        RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter!");
        RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter!");
        RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Password must contain at least 1 digit!");
        RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 special character!");
    }

}
