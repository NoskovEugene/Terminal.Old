using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Terminal.SharedModels.Services.Logging;

public class LoggerService : ILoggerService
{
    private const string PATTERN = "[{Timestamp:HH:mm:ss} {Level:u3}]>_ {Message:lj}{NewLine}";
 
    public ILogger ConsoleLogger { get; private set; }

    public ILogger FileLogger { get; private set; }

    public LoggerService()
    {
        ConsoleLogger = new LoggerConfiguration().WriteTo
            .Console(LogEventLevel.Debug, PATTERN, theme: SystemConsoleTheme.Colored).CreateLogger();
        var cfg = new LoggerConfiguration().WriteTo.File($@".\logs\{GetFileLogName()}", LogEventLevel.Debug, PATTERN);
        FileLogger = cfg.CreateLogger();
    }

    public void UpdateConsoleLogger(string pattern, Action<LoggerConfiguration> updateAction)
    {
        var config =
            new LoggerConfiguration().WriteTo.Console(LogEventLevel.Debug,
                pattern
                    .Trim(),
                theme: SystemConsoleTheme.Colored);
        updateAction(config);
        ConsoleLogger = config.CreateLogger();
    }

    public void UpdateFileLogger(Action<LoggerConfiguration> updateAction)
    {
        var cfg = new LoggerConfiguration().WriteTo.File($"/logs/{GetFileLogName()}");
        updateAction(cfg);
        FileLogger = cfg.CreateLogger();
    }

    private string GetFileLogName()
    {
        var now = DateTime.Now;
        return $"{now.Day}{now.Month}{now.Year}.txt";
    }
}