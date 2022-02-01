// See https://aka.ms/new-console-template for more information


using Routing.Models;
using Routing.Parsers;
using SharedModels.Attributes.UtilityAttributes;

namespace Terminal;
public static class Program
{
    public static void Main(string[] args)
    {
        /*var line = "test.add \"multiparameter1 multiparameter2\"; param2; -f -g";
        var context = new ParsingContext()
        {
            UnparsedLine = line,
            CurrentStepLine = line
        };
        var utilParser = new DefaultUtilityParser();
        context = utilParser.Parse(context);
        var flagParser = new DefaultFlagParser();
        context = flagParser.Parse(context);
        var parameterParser = new DefaultParameterParser();
        context = parameterParser.Parse(context);
        Console.WriteLine(context.CurrentStepLine);*/

        var line = "todo.add \"long parameter\"; parameter1; parameter2; -f -d -c";
        var context = new ParsingContext()
        {
            UnparsedLine = line,
            CurrentStepLine = line
        };
        var utilityParser = new DefaultUtilityParser();
        var parameterParser = new DefaultParameterParser();
        var flagParser = new DefaultFlagParser();

        context = utilityParser.Parse(context);
        context = flagParser.Parse(context);
        context = parameterParser.Parse(context);
        
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