using Microsoft.AspNetCore.Hosting.Server.Features;
using Serilog;

namespace API.Extensions.WebApplicationExtensions;

public static class ConfigureExtensions
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
            });
        }

        public void ApplicationStarted()
        {
            app.Lifetime.ApplicationStarted.Register(app.PrintAddresses);
            Log.Information("Application started");
        }

        private void PrintAddresses()
        {
            var addressFeature = app.GetAddressFeature();
            if (addressFeature == null && addressFeature!.Addresses.Count == 0) return;
            var address = addressFeature.Addresses.First().LocalHostReplace();
            PrintHelloLog(address);
        }

        private IServerAddressesFeature? GetAddressFeature()
        {
            var server = app.Services.GetRequiredService<Microsoft.AspNetCore.Hosting.Server.IServer>();
            return server.Features.Get<IServerAddressesFeature>();
        }
    }

    private static string LocalHostReplace(this string address)
    {
        return address
            .Replace("[::]", "localhost")
            .Replace("0.0.0.0", "localhost");
    }

    private static void PrintHelloLog(string address)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  🚀 The server has started successfully!");
        Console.ResetColor();

        Console.WriteLine($"  🌐 Vgt7 Web UI: {address}");
        Console.WriteLine($"  📚 Swagger UI:  {address}/swagger/index.html");
        Console.WriteLine();
    }
}