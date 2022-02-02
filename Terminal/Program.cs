// See https://aka.ms/new-console-template for more information


using System.Reflection;
using Common.Extensions.List;
using Routing.Extensions;
using Routing.Models;
using Routing.Parsers;
using Routing.Scanner;
using SharedModels.Attributes.UtilityAttributes;

namespace Terminal;
public static class Program
{
    public static void Main(string[] args)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyScanner = new AssemblyScanner();
        var utilities = assemblyScanner.Scan(assembly);
        var context = ParseLine("test.add param1; param2; -f", Rules);
        
        
        
        Console.ReadKey();
    }

    public static List<IParser> Rules = new()
    {
        new DefaultUtilityParser(),
        new DefaultParameterParser(),
        new DefaultFlagParser()
    };

    public static ParsingContext ParseLine(string line, IEnumerable<IParser> rules)
    {
        var context = new ParsingContext
        {
            UnparsedLine = line,
            CurrentStepLine = line
        };
        rules.Foreach(x => context = x.Parse(context));
        return context;
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