using GenericRepository;
using PChat.Application.Services;
using PChat.Persistance.Context;
using PChat.Persistance.Services;
using PChat.WebAPI.Middleware;

namespace PChat.WebAPI.Configurations;

public sealed class PersistanceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddTransient<ExceptionMiddleware>();
        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
    }
}
