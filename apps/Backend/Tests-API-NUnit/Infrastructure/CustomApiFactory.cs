using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;

namespace API_NUnit_Tests.Infrastructure;

public class CustomApiFactory : WebApplicationFactory<Program>
{
    public readonly PostgreSqlContainer DbContainer = new PostgreSqlBuilder("timescale/timescaledb:latest-pg18")
        .WithDatabase("test_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
            
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(DbContainer.GetConnectionString()));
        });
    }
}