using MediatR;
using PChat.Domain.Dto;

namespace PChat.Application.Features.AuthFeatures.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string UserName,
    string FullName,
    string Password): IRequest<MessageResponse>;
