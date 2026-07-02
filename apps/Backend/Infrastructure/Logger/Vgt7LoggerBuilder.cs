using Infrastructure.Options;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace Infrastructure.Logger;

public class Vgt7LoggerBuilder(IOptions<ConnectionOptions> options)
{
    private readonly ConnectionOptions _options = options.Value;

    public Serilog.Core.Logger Build()
    {
        var columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            ["message"] = new RenderedMessageColumnWriter(),
            ["level"] = new LevelColumnWriter(true, NpgsqlDbType.Varchar),
            ["timestamp"] = new TimestampColumnWriter(),
            ["exception"] = new ExceptionColumnWriter(),
            ["properties"] = new LogEventSerializedColumnWriter()
        };
        
        Serilog.Debugging.SelfLog.Enable(Console.Error);
        
        var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.WithProperty("Application", "API")
            .Enrich.WithProperty("Application", "Infrastructure")
            .WriteTo.PostgreSQL(
                connectionString: _options.Psql,
                tableName: "logs",
                columnOptions: columnWriters,
                needAutoCreateTable: false)
            .WriteTo.Console()
            .CreateLogger();
        
        Log.Logger = logger;
        Log.Information("Logger init");
        return logger;
    }
}