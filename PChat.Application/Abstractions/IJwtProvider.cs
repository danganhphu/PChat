﻿using PChat.Application.Features.AuthFeatures.Commands.Login;
using PChat.Domain.Entities;

namespace PChat.Application.Abstractions;

public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateTokenAsync(User user);
}
