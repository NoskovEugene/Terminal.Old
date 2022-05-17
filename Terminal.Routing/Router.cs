using Terminal.SemanticAnalyzer.Models;
using Serilog;
using Terminal.SharedModels.Models.Routing.Scanner;

namespace Terminal.Routing;


public class Router : IRouter
{
    private readonly List<Route> _routes;

    private readonly ILogger _logger;

    public Router(ILogger logger)
    {
        _logger = logger;
        _routes = new();
    }

    public void AppendUtilities(List<Utility> utilities)
    {
        utilities.ForEach(x =>
        {
            var route = _routes.FirstOrDefault(route => route.UtilityName == x.Name);
            if (route != null)
            {
                route.Commands.AddRange(x.Commands);
            }
            else
            {
                _routes.Add(new Route
                {
                    UtilityName = x.Name,
                    Commands = new List<Command>(x.Commands)
                });
            }
        });
    }

    public bool RemoveRoute(string utilityName)
    {
        var route = _routes.FirstOrDefault(x => x.UtilityName == utilityName);
        if (route == null) return false;
        _routes.Remove(route);
        return true;
    }

    public List<Route> GetAllRoutes() => _routes;
    public Command? FindByUtilAndCommand(string utilName, string commandName)
    {
        return _routes.FirstOrDefault(x => x.UtilityName == utilName)!.Commands
            .FirstOrDefault(x => x.Name == commandName);
    }

    public List<Command> FindCommands(ParsingContext context)
    {
        var route = _routes.FirstOrDefault(x => x.UtilityName == context.ParsedUtilityName);
        if (route == null)
        {
            _logger.Fatal("Route not found");
            return default!;
        }
        var commands = route.Commands.Where(x =>
                x.Name == context.ParsedCommandName && x.Parameters.Count == context.ParsedParameters.Count)
            .ToList();
        for (var i = 0; i < commands.Count; i++)
        {
            var command = commands[i];
            if (!CompareCollectionOfParameters(context.ParsedParameters, command.Parameters.ToList()))
            {
                commands.Remove(command);
            }
        }
        return commands;
    }

    private bool CompareCollectionOfParameters(List<ParsedParameter> parsedParameters, List<Parameter> parameters)
    {
        for (var i = 0; i < parsedParameters.Count; i++)
        {
            var parsedParameter = parsedParameters[i];
            var parameter = parameters[i];
            if (!CompareParameters(parsedParameter, parameter))
                return false;
        }

        return true;
    }

    private bool CompareParameters(ParsedParameter parsedParameter, Parameter parameter)
    {
        if (parsedParameter.ParameterTypeEnum == ParsedParameterTypeEnum.Array)
        {
            var internalParameters = (List<ParsedParameter>)parsedParameter.Value;
            var elementType = parameter.Type.GetElementType();

            foreach (var internalParameter in internalParameters)
            {
                if (!CompareParameters(internalParameter, new() {Type = elementType})) return false;
            }
            var objectValues = internalParameters.Select(x => Convert.ChangeType(x.Value, elementType)).ToArray();
            var targetArray = Array.CreateInstance(elementType, objectValues.Length);
            Array.Copy(objectValues, targetArray, objectValues.Length);
            parsedParameter.Value = targetArray;
            return true;
        }

        if (!TryConvertToType(parsedParameter.Value, parameter.Type, out var objectResult)) return false;
        parsedParameter.Value = objectResult;
        return true;
    }

    private bool TryConvertToType(object input, Type targetType, out object value)
    {
        try
        {
            value = Convert.ChangeType(input, targetType);
            return true;
        }
        catch (Exception e)
        {
            value = null;
            return false;
        }
    }
}