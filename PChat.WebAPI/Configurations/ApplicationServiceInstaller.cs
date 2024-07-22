using FluentValidation;
using MediatR;
using PChat.Application.Behaviors;

namespace PChat.WebAPI.Configurations;

public sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddMediatR(cfr =>
            cfr.RegisterServicesFromAssembly(typeof(PChat.Application.AssemblyReference).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(PChat.Application.AssemblyReference).Assembly);
    }
}
