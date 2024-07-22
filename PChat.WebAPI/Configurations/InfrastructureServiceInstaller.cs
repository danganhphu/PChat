namespace PChat.WebAPI.Configurations;

public sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        // services.AddScoped<IJwtProvider, JwtProvider>();
        //
        // services.ConfigureOptions<JwtOptionsSetup>();
        // services.ConfigureOptions<JwtBearerOptionsSetup>();
    }
}
