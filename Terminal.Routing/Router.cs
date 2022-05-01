﻿using Terminal.SemanticAnalyzer.Models;
using Serilog;
using Terminal.SharedModels.Models.Routing.Scanner;
using Terminal.Common.Extensions.List;
using Terminal.Routing.Services.Parameter;
using Terminal.Routing.Services.Parameter.ParameterAnalyze;

namespace Terminal.Routing;


public class Router : IRouter
{
    private readonly List<Route> _routes;

    private readonly ILogger _logger;

    private readonly IParameterAnalyzeService _parameterAnalyzeService;

    public Router(ILogger logger, IParameterAnalyzeService parameterAnalyzeService)
    {
        _logger = logger;
        _routes = new();
        _parameterAnalyzeService = parameterAnalyzeService;
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
        for (int i = 0; i < context.ParsedParameters.Count; i++)
        {
            var parsedParameter = context.ParsedParameters[i];
            for (int j = 0; j < commands.Count; j++)
            {
                var command = commands[j];
                
            }
        }
        return commands;
    }
}