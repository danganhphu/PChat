using PChat.Application.Features.AuthFeatures.Commands.Login;
using PChat.Domain.Entities;

namespace PChat.Application.Abstractions.JWT;

public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateTokenAsync(User user);
}
