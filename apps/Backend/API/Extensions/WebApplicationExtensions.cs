using Infrastructure.Logger;
using Serilog;

namespace API.Extensions;

public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public void ConfigureDevelopment()
        {
            if (!app.Environment.IsDevelopment()) return;

            app.MapOpenApi();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
                options.RoutePrefix = "swagger";
                var hostAddress = app.Configuration["API_HostAddress"] ?? "http://localhost:5250";
                Console.WriteLine($"Swagger UI: {hostAddress}/swagger/index.html");
            });
        }

        public void ConfigureServices()
        {
            var cfg = app.Configuration;

            Log.Logger = Vgt7Logger.Create(cfg["PSQL"]);
        }
    }
}