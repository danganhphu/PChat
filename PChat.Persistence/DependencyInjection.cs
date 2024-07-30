using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PChat.Application.Abstractions.Data;
using PChat.Application.Services;
using PChat.Domain.Entities;
using PChat.Domain.Interface;
using PChat.Persistence.Context;
using PChat.Persistence.Repositories;
using PChat.Persistence.Services;

namespace PChat.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbDevConnection")));

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        #region Repositories

        services.AddTransient<IUserRepository, UserRepository>();

        #endregion

        return services;
    }
}