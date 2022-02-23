using SemanticAnalyzer.Exceptions;
using SemanticAnalyzer.Models;

namespace SemanticAnalyzer.DefaultParsers;

public class DefaultUtilityParser : IParser
{
    private const string ParserStep = "Utility";
    
    public void Parse(ref ParsingContext context)
    {
        context.CurrentStep ??= context.UnparsedLine;
        if (string.IsNullOrWhiteSpace(context.CurrentStep)) 
            throw new ParsingException(ParserStep, "Input line cannot be empty");
        var index = context.CurrentStep.IndexOf(' ');
        var utility = index != -1 ? context.CurrentStep[..index] : context.CurrentStep;
        var array = utility.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (array.Length != 2) throw new ParsingException(ParserStep, "Utility or command not found");
        context.ParsedUtilityName = array[0];
        context.ParsedCommandName = array[1];
        context.CurrentStep = context.CurrentStep.Replace(utility, string.Empty).Trim(' ');
    }
}