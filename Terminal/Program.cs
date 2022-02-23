// See https://aka.ms/new-console-template for more information

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using SemanticAnalyzer;
using SemanticAnalyzer.DefaultParsers;
using Serilog;
using SharedModels.Attributes.UtilityAttributes;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        var line = "test.add \"long parameter\" [param1 param2 param3 param1 param2 -f -g -h -s";
        var service = SemanticService();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .Build();
        Console.ReadKey();
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