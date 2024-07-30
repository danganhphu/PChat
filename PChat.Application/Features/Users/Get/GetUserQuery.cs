using MediatR;
using PChat.Domain.Dto;

namespace PChat.Application.Features.Users.Get;

public record GetUserQuery(UserId UserId) : IRequest<UserResponse>;

public record UserResponse(
    string Id,
    string Email,
    string FullName,
    string Phone);