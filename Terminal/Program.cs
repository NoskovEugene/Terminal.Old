// See https://aka.ms/new-console-template for more information

using System.Reflection;

using Routing.Scanner;

using SemanticAnalyzer;
using SemanticAnalyzer.DefaultParsers;
using SharedModels.Attributes.UtilityAttributes;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        /*var line = "test.add \"long parameter\" [param1 param2 param3] param1 param2 -f -g -h -s";
        var service = SemanticService();
        var context = service.ParseInputLine(line);*/
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();
        var scanner = new AssemblyScanner();
        var utility = scanner.ScanAssembly(assembly);
    }

    public static ISemanticService SemanticService()
    {
        var service = new SemanticService();
        service.AddParser<DefaultUtilityParser>();
        service.AddParser<DefaultParameterParser>();
        service.AddParser<DefaultFlagParser>();
        return service;
    }
}

[Utility("test")]
public class TestUtility
{
    public TestUtility()
    {
    }

    [Command("add")]
    public void TestMethod(byte parameter1, int parameter2, [Flag]string[] flags)
    {
    }
}