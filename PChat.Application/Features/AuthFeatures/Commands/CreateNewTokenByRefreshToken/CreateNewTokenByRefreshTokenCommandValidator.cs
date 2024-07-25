using FluentValidation;

namespace PChat.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;

public sealed class CreateNewTokenByRefreshTokenCommandValidator : AbstractValidator<CreateNewTokenByRefreshTokenCommand>
{
    public CreateNewTokenByRefreshTokenCommandValidator()
    {
        RuleFor(p => p.UserId).NotEmpty().WithMessage("User information cannot be empty!");
        RuleFor(p => p.UserId).NotNull().WithMessage("User information cannot be empty!");
        RuleFor(p => p.RefreshToken).NotNull().WithMessage("Refresh Token information cannot be empty!");
        RuleFor(p => p.RefreshToken).NotEmpty().WithMessage("Refresh Token information cannot be empty!");
    }
}
