using Common.Extensions.List;
using SemanticAnalyzer.Models;
using Serilog;
using SharedModels.Models.Routing.Scanner;

namespace Routing;

public class Router : IRouter
{
    private readonly List<Utility> _utilities;

    private readonly ILogger _logger;

    public Router(ILogger logger)
    {
        _logger = logger;
        _utilities = new();
    }

    public void AppendUtilities(List<Utility> utilities)
    {
        utilities.Foreach(x =>
        {
            var util = _utilities.FirstOrDefault(utility => utility.Name == x.Name);
            if(util == null)
                _utilities.Add(x);
        });
    }

    public Utility? FindUtility(ParsingContext context)
    {
        var util = _utilities.FirstOrDefault(x => x.Name == context.ParsedUtilityName);
        return util ?? null;
    }

    public List<Command> FindCommands(Utility util, ParsingContext context)
    {
        var commands = util.Commands.Where(x =>
                x.Name == context.ParsedCommandName && x.Parameters.Count == context.ParsedParameters.Count)
            .ToList(); 
        foreach (var parsedParameter in context.ParsedParameters)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                var command = commands[i];
                command.Parameters.Foreach(parameter =>
                {
                    if (!CheckPossibleParameterType(parsedParameter, parameter))
                    {
                        commands.Remove(command);
                    }
                });
            }
        }
        return commands;
    }
    
    private bool CheckPossibleParameterType(ParsedParameter parsedParameter, Parameter parameter)
    {
        try
        {
            var targetType = parameter.Type;
            parsedParameter.Value = Convert.ChangeType(parsedParameter.Value, targetType);
            return true;
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return false;
        }
    }

}