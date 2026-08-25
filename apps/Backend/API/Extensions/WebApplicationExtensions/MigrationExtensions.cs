using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions.WebApplicationExtensions;

public static class MigrationExtensions
{
    extension(WebApplication app)
    {
        public void ApplyMigrations()
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>(); // Укажите ваш класс контекста
                    context.Database.Migrate(); 
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Migrations failed.");
                    throw;
                }
            }
        }
    }
}