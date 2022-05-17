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
        var core = new TerminalCore();
        var logger = core.Container.Resolve<ILogger>();
        logger.Information("Use 'allcommands' for view all commands");
        core.StartListen();
    }
}
