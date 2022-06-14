using Terminal.SemanticAnalyzer.Exceptions;
using Terminal.SemanticAnalyzer.Models;

namespace Terminal.SemanticAnalyzer.DefaultParsers;

public class DefaultUtilityParser : IParser
{
    private const string ParserStep = "Utility";

    private readonly Dictionary<int, Action<string[], ParsingContext>> parsingRules = new()
    {
        {1, (strings, context) => context.ParsedCommandName = strings[0]},
        {2, (strings, context) =>
        {
            context.ParsedUtilityName = strings[0];
            context.ParsedCommandName = strings[1];
        }}
    };


    public void Parse(ref ParsingContext context)
    {
        context.CurrentStep ??= context.UnparsedLine;
        if (string.IsNullOrWhiteSpace(context.CurrentStep)) 
            throw new ParsingException(ParserStep, "Input line cannot be empty");
        var index = context.CurrentStep.IndexOf(' ');
        var utility = index != -1 ? context.CurrentStep[..index] : context.CurrentStep;
        var array = utility.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (array.Length <= 0) throw new ParsingException(ParserStep, "Utility or command not found");
        if (parsingRules.ContainsKey(array.Length))
        {
            parsingRules[array.Length](array, context);
        }
        else
        {
            parsingRules[2](array, context);
        }
        context.CurrentStep = context.CurrentStep.Remove(0, utility.Length).Trim(' ');
    }
}