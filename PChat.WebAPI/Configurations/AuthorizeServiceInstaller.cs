﻿namespace PChat.WebAPI.Configurations;

public sealed class AuthorizeServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddAuthentication().AddJwtBearer();
        services.AddAuthorization();
    }
}
