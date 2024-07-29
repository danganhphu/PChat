using System.Globalization;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using PChat.Application.Behaviors;
using PChat.Application.Contracts.Localization;

namespace PChat.WebAPI.Configurations;

public sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddMediatR(cfr =>
            cfr.RegisterServicesFromAssembly(typeof(PChat.Application.AssemblyReference).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(PChat.Application.AssemblyReference).Assembly);
        services.AddDistributedMemoryCache();
        services.AddLocalization();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US")
            };
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }
}
