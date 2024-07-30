using PChat.Application;
using PChat.Application.Hubs;
using PChat.Infrastructure;
using PChat.Persistence;
using PChat.WebAPI.Extensions;
using PChat.WebAPI.Middleware;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("starting server.");
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.WriteTo.Console();
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });
    
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddPersistence(builder.Configuration);
    
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSignalR();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.ApplyMigrations();
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseAuthorization();
    app.MapControllers();
    app.MapHub<ChatHub>("/chatHub");
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}