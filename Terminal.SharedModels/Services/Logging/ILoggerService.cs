using Serilog;

namespace Terminal.SharedModels.Services.Logging;

public interface ILoggerService
{
    ILogger ConsoleLogger { get; }
    ILogger FileLogger { get; }
    void UpdateConsoleLogger(string pattern, Action<LoggerConfiguration> updateAction);
    void UpdateFileLogger(Action<LoggerConfiguration> updateAction);
}