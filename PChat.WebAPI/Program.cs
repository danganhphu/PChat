using Microsoft.EntityFrameworkCore;
using PChat.Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(PChat.Presentation.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbDevConnection")));
builder.Services.AddMediatR(x=>x.RegisterServicesFromAssemblies(typeof(PChat.Application.AssemblyReference).Assembly));
// builder.Services.AddAutoMapper(typeof(PChat.Persistance.AssemblyRefence).Assembly);

builder.Services.AddControllers();
// builder.Services.InstallServices(
//     configuration: builder.Configuration,
//     hostBuilder: builder.Host,
//     assemblies: new Assembly[]
//     {
//         typeof(ApplicationServiceInstaller).Assembly,
//         typeof(PersistanceServiceInstaller).Assembly
//     });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
