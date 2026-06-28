using API.Extensions;
using Infrastructure.Logger;
using Serilog;

Log.Information("Starting application");

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Log.Logger = Vgt7Logger.Create(connectionString ?? "empty-test");

builder.Services.AddApplicationServices();
builder.Host.ConfigureHost();

var app = builder.Build();
app.ConfigureDevelopment();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

Log.CloseAndFlush();