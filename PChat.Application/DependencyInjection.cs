using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PChat.Application.Behaviors;
using PChat.Application.Mappings;

namespace PChat.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);
        
        services.AddAutoMapper(typeof(MappingProfile));

        
        return services;
    }
}