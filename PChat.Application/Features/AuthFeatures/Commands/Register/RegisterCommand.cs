using MediatR;

namespace PChat.Application.Features.AuthFeatures.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string UserName,
    string FullName,
    string Password): IRequest<RegisterResponse>;

public sealed record RegisterRequest(
    string Email,
    string UserName,
    string FullName,
    string Password);