using System.Reflection;
using Terminal.Core;
using Terminal.Routing;
using Terminal.Routing.Scanner;
using Terminal.Routing.Services.Parameter;
using Terminal.SemanticAnalyzer;
using Terminal.SemanticAnalyzer.DefaultParsers;
using Terminal.SemanticAnalyzer.Models;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        var core = new TerminalCore();
        var router = core.Container.Resolve<IRouter>();
        var scanner = core.Container.Resolve<IAssemblyScanner>();
        var analyzer = core.Container.Resolve<ISyntaxAnalyzer>();
        var utils = scanner.ScanAssembly(Assembly.GetExecutingAssembly());
        var context = analyzer.ParseInputLine("test.remove [123 123,123 123 123,123]");
        router.AppendUtilities(utils);
        var parameterService = new ParameterService();
        parameterService.ChangeParametersToPossibleType(context);
        var lst = parameterService.PrepareParsedParameters(context);
        
        Console.ReadKey();
    }
}


public class TestClass
{
    public void Method(byte param)
    {
        Console.WriteLine("123");
    }
}
