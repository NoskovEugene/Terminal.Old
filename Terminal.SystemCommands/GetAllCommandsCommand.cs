using Serilog;
using Terminal.Routing;
using Terminal.SharedModels.Attributes.UtilityAttributes;
using Terminal.SystemCommands.Attributes;

namespace Terminal.SystemCommands;

[SystemUtility]
public class GetAllCommandsCommand
{
    private ILogger _logger;
    private IRouter _router;

    public GetAllCommandsCommand(ILogger logger, IRouter router)
    {
        _logger = logger;
        _router = router;
    }
    
    [Command("allcommands")]
    public void AllCommands()
    {
        var routes = _router.GetAllRoutes();
        foreach (var route in routes)
        {
            _logger.Information($"{route.UtilityName}");
            foreach (var routeCommand in route.Commands)
            {
                _logger.Information($"->{routeCommand.Name}");
            }
        }
    }
    
    [Command("test")]
    public void Test(double val, string[] flags)
    {
        Console.WriteLine(val.GetType());
        Console.WriteLine(val);
    }
}
