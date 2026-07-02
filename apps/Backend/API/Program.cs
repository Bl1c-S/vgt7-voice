using API.Extensions;
using Serilog;

Log.Information("Starting application");

var builder = WebApplication.CreateBuilder(args);
builder.AddApplicationServices();
var app = builder.Build();

app.ConfigureDevelopment();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

Log.CloseAndFlush();