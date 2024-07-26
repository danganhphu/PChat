using PChat.Application.Bases;
using PChat.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using PChat.Application.Features.AuthFeatures.Commands.Login;
using PChat.Application.Features.AuthFeatures.Commands.Register;

namespace PChat.Application.Services;

public interface IAuthService
{
    Task<BaseResponse<RegisterResult>> RegisterAsync(RegisterCommand request);
    Task<BaseResponse<LoginCommandResponse>> LoginAsync(LoginCommand request, CancellationToken cancellationToken);
    Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken);

}
