using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace Infrastructure.Logger;

public static class Vgt7Logger
{
    public static Serilog.Core.Logger Create(string? connectionString)
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
                connectionString: connectionString,
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