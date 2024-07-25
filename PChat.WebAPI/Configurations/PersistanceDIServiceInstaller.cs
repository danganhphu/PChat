using PChat.WebAPI.Middleware;

namespace PChat.WebAPI.Configurations;

public sealed class PersistanceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        // services.AddScoped<IAuthService, AuthService>();
        // services.AddScoped<IMailService, MailService>();
        // services.AddScoped<IRoleService, RoleService>();
        // services.AddScoped<IUserRoleService, UserRoleService>();

        // services.AddTransient<ExceptionMiddleware>();
        // services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
        // services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    }
}
