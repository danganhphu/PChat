using MediatR;
using PChat.Application.Bases;

namespace PChat.Application.Features.AuthFeatures.Commands.Login;

public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password) : IRequest<BaseResponse<LoginCommandResponse>>;
