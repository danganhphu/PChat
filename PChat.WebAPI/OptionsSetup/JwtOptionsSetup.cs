﻿using Microsoft.Extensions.Options;
using PChat.Infrastructure.Authenticaton;

namespace PChat.WebAPI.OptionsSetup;

public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection("Jwt").Bind(options);
    }
}
