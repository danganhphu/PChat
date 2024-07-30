using PChat.Application.Services;
using MediatR;

namespace PChat.Application.Features.AuthFeatures.Commands.Login;

public sealed class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request, cancellationToken);
    }
}
