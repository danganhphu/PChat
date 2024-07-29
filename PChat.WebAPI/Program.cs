using PChat.Application.Hubs;
using PChat.WebAPI.Configurations;
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
    // builder.Services.InstallServices(
    //     builder.Configuration,
    //     builder.Host,
    //     Assembly.GetExecutingAssembly()
    // );
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSignalR();
    builder.Services.InstallServices(builder.Configuration, builder.Host,
        typeof(ApplicationServiceInstaller).Assembly,
        typeof(AuthorizeServiceInstaller).Assembly,
        typeof(InfrastructureServiceInstaller).Assembly,
        typeof(PersistenceDIServiceInstaller).Assembly,
        typeof(PersistenceServiceInstaller).Assembly,
        typeof(PresentationServiceInstaller).Assembly);

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseMiddlewareExtensions();
    app.UseHttpsRedirection();

    #region Localizer with JSON

    var supportedCultures = new[] { "en-US" };
    var localizationOptions = new RequestLocalizationOptions()
        .SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
    localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
    app.UseRequestLocalization(localizationOptions);
    app.UseRouting();

    #endregion
    
    app.UseAuthorization();
    app.UseAuthorization();
    app.MapControllers();
    app.MapHub<ChatHub>("/chatHub");
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