using Microsoft.Extensions.DependencyInjection;
using PChat.Application.Abstractions.JWT;
using PChat.Infrastructure.Authenticaton;
using PChat.Infrastructure.OptionsSetup;

namespace PChat.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddAuthentication().AddJwtBearer();
        services.AddAuthentication().AddJwtBearer();
        services.AddAuthorization();
        
        return services;
    }
}