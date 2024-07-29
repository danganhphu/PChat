using PChat.Application.Services;
using PChat.Domain.Interfaces;
using PChat.Persistence.Context;
using PChat.Persistence.Repositories;
using PChat.Persistence.Services;
using PChat.Persistence.UnitOfWorks;
using PChat.WebAPI.Middleware;

namespace PChat.WebAPI.Configurations;

public sealed class PersistenceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUser, CurrentUser>();
        services.AddTransient<ExceptionMiddleware>();
        #region Repositories
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<ICallRepository, CallRepository>();
        #endregion
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}
