using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PChat.Persistance.Context;
using PChat.WebAPI.Configurations;
using PChat.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);


// builder.Services.InstallServices(builder.Configuration, builder.Host,
//     typeof(ApplicationServiceInstaller).Assembly,
//     typeof(AuthorizeServiceInstaller).Assembly,
//     typeof(InfrastructureServiceInstaller).Assembly,
//     typeof(PersistanceDIServiceInstaller).Assembly,
//     typeof(PersistanceServiceInstaller).Assembly,
//     typeof(PresentationServiceInstaller).Assembly);

builder.Services.InstallServices(
    builder.Configuration,
    builder.Host,
    Assembly.GetExecutingAssembly() // Cung cấp các assembly chứa các IServiceInstaller
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();