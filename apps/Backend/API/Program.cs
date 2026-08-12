using API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplicationServices();
//TODO Serilog.Sinks.PostgreSQL.PostgreSQLSink: Npgsql.NpgsqlException (0x80004005): Resource temporarily unavailable
// Docker-compose psql doesn't work'

var app = builder.Build();
app.ConfigureDevelopment();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.ApplicationStarted();
app.Run();