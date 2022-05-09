using System.Reflection;
using Castle.MicroKernel.Registration;
using Serilog;
using Terminal.Common.Extensions;
using Terminal.Core;
using Terminal.Routing;
using Terminal.Routing.Scanner;
using Terminal.SemanticAnalyzer;
using Terminal.SemanticAnalyzer.Models;
using Terminal.SystemCommands;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        var systemCommands = new List<Type>()
        {
            typeof(RegisterCommand)
        };
        var core = new TerminalCore();
        var scanner = core.Container.Resolve<IAssemblyScanner>();
        var utils = scanner.ScanTypes(systemCommands.ToArray());
        var router = core.Container.Resolve<IRouter>();
        router.AppendUtilities(utils);
        foreach (var systemCommand in systemCommands)
        {
            core.Container.Register(Component.For(systemCommand).ImplementedBy(systemCommand));
        }

        var analyzer = core.Container.Resolve<ISyntaxAnalyzer>();
        Console.Write(">_");
        var ctx = analyzer.ParseInputLine(Console.ReadLine());
        var command = router.FindCommands(ctx)[0];
        var instance = core.Container.Resolve(command.ClassInfo);
        var parameters = ctx.ParsedParameters.Select(x => x.Value).ToList();
        if (ctx.ParsedFlags.Count > 0)
        {
            parameters.Add(ctx.ParsedFlags.ToArray());
        }

        command.MethodInfo.Invoke(instance, parameters.ToArray());
        Console.ReadKey();
    }
}
