using System.Reflection;
using Terminal.Common.MapService;
using Terminal.Common.MapService.Helpers;
using Terminal.Core;
using Terminal.Routing;
using Terminal.Routing.Scanner;
using Terminal.SemanticAnalyzer;
using Terminal.SharedModels.Models.Routing.Scanner;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        var core = new TerminalCore();
        var scanner = core.Container.Resolve<IAssemblyScanner>();
        var analyzer = core.Container.Resolve<ISyntaxAnalyzer>();
        var utils = scanner.ScanAssembly(Assembly.GetExecutingAssembly());
        var context = analyzer.ParseInputLine("remove -123 [-123 123,123 -123 123,123]");
        
    }
}

public class Route
{
    public Route(string utilityName, List<Command> commands)
    {
        UtilityName = utilityName;
        Commands = commands;
    }

    public string UtilityName { get; }
    
    public List<Command> Commands { get; }
    
    
}

public class Router
{

    private List<Route> _routes;

    public Router()
    {
        _routes = new();
    }
    
    public void AppendUtils(List<Utility> utilities)
    {
        var groupedUtils = utilities.GroupBy(x => x.Name);
        foreach (var groupedUtil in groupedUtils)
        {
            var commands = groupedUtil.SelectMany(x => x.Commands).ToList();
            var utilName = groupedUtil.Key;
            if (_routes.Any(x => x.UtilityName == utilName))
            {
                var util = _routes.First(x => x.UtilityName == utilName);
                var diff = util.Commands.Except(commands);
                util.Commands.AddRange(diff);
            }
            else
            {
                var util = new Route(utilName, commands);
                _routes.Add(util);
            }
        }
    }
}

