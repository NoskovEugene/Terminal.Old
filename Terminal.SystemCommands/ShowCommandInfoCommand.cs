using Serilog;
using Terminal.Routing;
using Terminal.SharedModels.Attributes.UtilityAttributes;
using Terminal.SystemCommands.Attributes;

namespace Terminal.SystemCommands;

[SystemUtility]
public class ShowCommandInfoCommand
{

    private readonly IRouter _router;

    private ILogger _logger;
    
    public ShowCommandInfoCommand(IRouter router, ILogger logger)
    {
        _router = router;
        _logger = logger;
    }

    [Command("commandinfo")]
    public void Show(string utilName, string commandName)
    {
        var command = _router.FindByUtilAndCommand(utilName, commandName);
        if (command != null)
        {
            _logger.Information($"Command name: {command.Name}");
            if (command.Parameters.Count > 0)
            {
                _logger.Information("Parameters");
                foreach (var commandParameter in command.Parameters)
                {
                    _logger.Information($"->Parameter: {commandParameter.Name} with type: {commandParameter.Type.FullName}");
                }                
            }
            if (command.Flags.Count > 0)
            {
                _logger.Information("Flags");
                command.Flags.ForEach(x =>
                {
                    _logger.Information($"->Flag: {x}");
                });    
            }            
        }
        else
        {
            _logger.Information("Command with this name not found");
        }
    }

    [Command("ci")]
    public void ShowShort(string utilName, string commandName)
    {
        Show(utilName, commandName);
    }

    [Command("ci")]
    public void ShowOneParameter(string utilWithCommand)
    {
        var array = utilWithCommand.Split(".", StringSplitOptions.RemoveEmptyEntries);
        if (array.Length < 1)
        {
            _logger.Error($"Value '{utilWithCommand}' is not utility name with command. Use this pattern 'UtilityName.CommandName'");
            return;
        } 
        
        Show(array[0], array[1]);        
    }
}