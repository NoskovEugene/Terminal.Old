// See https://aka.ms/new-console-template for more information

using SemanticAnalyzer.DefaultParsers;
using SemanticAnalyzer.Models;
using SharedModels.Attributes.UtilityAttributes;

namespace Terminal;
public static class Program
{
    public static void Main(string[] args)
    {
        var context = new ParsingContext()
        {
            UnparsedLine = "param1; param2",
            CurrentStep = "test.add \"long parameter\" [param1 param2 param3] param1 param2 -f -g -h -s"
        };
        var utilParser = new DefaultUtilityParser();
        var paramParser = new DefaultParameterParser();
        var flagParser = new DefaultFlagParser();
        utilParser.Parse(ref context);
        paramParser.Parse(ref context);
        flagParser.Parse(ref context);
        Console.ReadKey();
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