using System.Reflection;
using Terminal.Routing.Extensions;
using Terminal.SharedModels.Attributes.UtilityAttributes;
using Terminal.SharedModels.Models.Routing.Scanner;
using Terminal.Common.Extensions.List;

namespace Terminal.Routing.Scanner;

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
                    Commands = GetCommands(assembly, x),
                };
                lst.Add(utility);
            }
        });
        return lst;
    }

    private List<Command> GetCommands(Assembly assemblyInfo, Type utilityType)
    {
        var lst = new List<Command>();
        utilityType.GetMethods().Foreach(info =>
        {
            if (info.IsCommand(out var commandAttribute))
            {
                var command = GetCommand(info, commandAttribute);
                command.ClassInfo = utilityType;
                command.AssemblyInfo = assemblyInfo;
                lst.Add(command);
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
            Flags = new List<Flag>(),
            MethodInfo = info
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