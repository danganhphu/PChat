using PChat.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using PChat.Application.Features.AuthFeatures.Commands.Login;
using PChat.Application.Features.AuthFeatures.Commands.Register;

namespace PChat.Application.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterCommand request);
    Task<LoginCommandResponse> LoginAsync(LoginCommand request, CancellationToken cancellationToken);
    Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken);

}
