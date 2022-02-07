// See https://aka.ms/new-console-template for more information

using System.Linq;
using Common.Extensions.List;
using SemanticAnalyzer;
using SemanticAnalyzer.DefaultParsers;
using SemanticAnalyzer.Models;
using SharedModels.Attributes.UtilityAttributes;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        var line = "test.add \"long parameter\" [param1 param2 param3] param1 param2 -f -g -h -s";
        var service = SemanticService();
        var context = service.ParseInputLine(line);
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
    public void TestMethod(string parameter1, string parameter2, string[] flags)
    {
    }
}