using System.Reflection;
using Terminal.Common.Extensions;
using Terminal.Core;
using Terminal.Routing;
using Terminal.Routing.Scanner;
using Terminal.SemanticAnalyzer;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        var core = new TerminalCore();
        var scanner = core.Container.Resolve<IAssemblyScanner>();
        var analyzer = core.Container.Resolve<ISyntaxAnalyzer>();
        var router = (Router)core.Container.Resolve<IRouter>();
        var utils = scanner.ScanAssembly(Assembly.GetExecutingAssembly());
        router.AppendUtilities(utils);
        var context = analyzer.ParseInputLine("test.remove [[123] [123]] 123");
        var result = router.FindCommands(context);
    }
}