using EntityFramework;
using Microsoft.OpenApi.Models;
using WebApiAuthenticate.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add repositories to the container.
services.InstallRepositories();
// Add services to the container.
services.InstallApplicationServices();
// Add infrastructure to the container.
services.InstallInfrastructureServices(configuration);

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(
c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Authenticate API",
        Description = "Authentication and registration API used for authentication, user creation and management (registration, password change, username change, email change, user deletion)"
    });
});
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

app.MigrateDatabase<UserDbContext>();

app.Run();