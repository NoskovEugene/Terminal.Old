using Serilog;
using Terminal.SharedModels.Services.Logging;

namespace UI.RequestService;

public class UserRequestService
{
    private readonly ILogger _logger;
    
    public UserRequestService(ILoggerService loggerService)
    {
        _logger = loggerService.ConsoleLogger;
    }

    public string RequestLine(string message)
    {
        PrepareInputField(message);
        return Console.ReadLine();
    }

    public T RequestBuiltIn<T>(string message)
    {
        while (true)
        {
            PrepareInputField(message);
            var line = Console.ReadLine();
            try
            {
                return (T) Convert.ChangeType(line, typeof(T));
            }
            catch (FormatException)
            {
                _logger.Warning("Wrong value format");
            }
            catch (Exception)
            {
                _logger.Warning($"Cant convert {line} to type {typeof(T).Name}");
            }
        }
    }

    public T SelectItem<T>(List<T> values, string message)
    {
        return SelectItem(values, message, (value) => value.ToString());
    }

    public T SelectItem<T>(List<T> values, string headerMessage, Func<T, string> customToString)
    {
        _logger.Information(headerMessage);
        for (var i = 0; i < values.Count; i++)
        {
            _logger.Information($"{i}) {customToString(values[i])}");
        }
        while (true)
        {
            var index = RequestBuiltIn<int>("Type index below");
            if (index >= 0 && index < values.Count)
            {
                return values[index];
            }
        }
    }

    private void PrepareInputField(string message)
    {
        if(!string.IsNullOrEmpty(message)) 
            _logger.Information(message);
        Console.Write(">_ ");
    }
}