using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PChat.Domain.Entities;
using PChat.Persistance.Context;
using Serilog;

namespace PChat.WebAPI.Configurations;

public sealed class PersistanceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddAutoMapper(typeof(PChat.Persistance.AssemblyRefence).Assembly);

        string connectionString = configuration.GetConnectionString("DbDevConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, 
                sqlOptions => sqlOptions.MigrationsAssembly(typeof(PChat.Persistance.AssemblyRefence).Assembly.FullName)));


        // services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<AppDbContext>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MSSqlServer(
                connectionString: connectionString,
                tableName: "Logs",
                autoCreateSqlTable: true)
            .CreateLogger();

        host.UseSerilog();
    }
}