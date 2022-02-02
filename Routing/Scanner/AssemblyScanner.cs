using System.Reflection;
using Common.Extensions.List;
using Routing.Extensions;
using Routing.Models;

namespace Routing.Scanner;

public class AssemblyScanner : IAssemblyScanner
{
    public List<Utility> Scan(Assembly assembly)
    {
        var utilities = new List<Utility>();
        var types = assembly.GetTypes();
        types.Foreach(x =>
        {
            if (x.IsUtility(out var utilityAttribute))
            {
                var utility = new Utility
                {
                    Name = utilityAttribute.UtilityName,
                    Commands = GetCommands(x.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                };
                utilities.Add(utility);
            }
        });
        return utilities;
    }
    
    private List<Command> GetCommands(MethodInfo[] methodInfos)
    {
        var commands = new List<Command>();
        methodInfos.Foreach(x =>
        {
            if (x.IsCommand(out var commandAttribute))
            {
                
                var command = new Command()
                {
                    Name = commandAttribute.CommandName,
                    Parameters = GetParameters(x.GetParameters())
                };
                commands.Add(command);
            }
        });
        return commands;
    }

    private List<Parameter> GetParameters(ParameterInfo[] parameterInfos)
    {
        var parameters = new List<Parameter>();
        parameterInfos.Foreach(x =>
        {
            var parameter = new Parameter()
            {
                Name = x.Name,
                ParameterType = x.ParameterType
            };
            parameters.Add(parameter);
        });
        return parameters;
    }
}