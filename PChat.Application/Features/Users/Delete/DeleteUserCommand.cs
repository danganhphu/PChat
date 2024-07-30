using MediatR;
using PChat.Domain.Dto;

namespace PChat.Application.Features.Users.Delete;

public record DeleteUserCommand(UserId UserId) : IRequest;
