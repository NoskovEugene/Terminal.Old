using System.Text.RegularExpressions;
using Terminal.SemanticAnalyzer.Exceptions;
using Terminal.SemanticAnalyzer.Models;

namespace Terminal.SemanticAnalyzer.DefaultParsers;

public class DefaultFlagParser : IParser
{
    private const string ParseStep = "Flag";
    
    public void Parse(ref ParsingContext context)
    {
        context.ParsedFlags ??= new();
        var counter = 0;
        while (ParseNextFlag(ref context))
        {
            counter += 1;
            if (counter >= 10) 
                throw new ParsingException(ParseStep, "To long parsing. Possible stack overflow error");
        } 
    }

    private bool ParseNextFlag(ref ParsingContext context)
    {
        var input = context.CurrentStep;
        if (string.IsNullOrWhiteSpace(input)) return false;
        var regex = new Regex(@"\-\w{1}");
        var match = regex.Match(input);
        if (match.Success)
        {
            context.ParsedFlags.Add(match.Value);
            context.CurrentStep = context.CurrentStep.Remove(0, match.Value.Length).Trim();
            return true;
        }
        return false;
    }
}