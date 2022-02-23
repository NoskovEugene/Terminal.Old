using System.Reflection;
using Common.Extensions.List;
using Routing.Extensions;
using SharedModels.Attributes.UtilityAttributes;
using SharedModels.Models.Routing.Scanner;

namespace Routing.Scanner;

public class AssemblyScanner : IAssemblyScanner
{
    public List<Utility> ScanAssembly(Assembly assembly)
    {
        var lst = new List<Utility>();
        var types = assembly.GetTypes();
        types.Foreach(x =>
        {
            if (x.IsUtility(out var attribute))
            {
                var utility = new Utility
                {
                    Name = attribute.UtilityName,
                    Commands = GetCommands(x)
                };
                lst.Add(utility);
            }
        });
        return lst;
    }

    private List<Command> GetCommands(Type utilityType)
    {
        var lst = new List<Command>();
        utilityType.GetMethods().Foreach(info =>
        {
            if (info.IsCommand(out var commandAttribute))
            {
                lst.Add(GetCommand(info, commandAttribute));
            }
        });
        return lst;
    }

    private Command GetCommand(MethodInfo info, CommandAttribute commandAttribute)
    {
        var command = new Command
        {
            Name = commandAttribute.CommandName,
            Parameters = new List<Parameter>(),
            Flags = new List<Flag>()
        };
        var parameters = info.GetParameters();
        parameters.Foreach(parameterInfo =>
        {
            if (parameterInfo.IsFlag(out var flagAttribute))
            {
                command.Flags.Add(new ()
                {
                    Name = parameterInfo.Name
                });
            }
            else
            {
                command.Parameters.Add(new()
                {
                    Name = parameterInfo.Name,
                    Type = parameterInfo.ParameterType
                });
            }
        });
        return command;
    }
}