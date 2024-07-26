using PChat.Application.Services;
using MediatR;
using PChat.Application.Bases;

namespace PChat.Application.Features.AuthFeatures.Commands.Login;

public sealed class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, BaseResponse<LoginCommandResponse>>
{
    public async Task<BaseResponse<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request, cancellationToken);
    }
}
