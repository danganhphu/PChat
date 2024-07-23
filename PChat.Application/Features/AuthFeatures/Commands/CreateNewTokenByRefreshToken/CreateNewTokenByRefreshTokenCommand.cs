using PChat.Application.Features.AuthFeatures.Commands.Login;
using MediatR;

namespace PChat.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;

public sealed record CreateNewTokenByRefreshTokenCommand(
    string UserId,
    string RefreshToken): IRequest<LoginCommandResponse>;
