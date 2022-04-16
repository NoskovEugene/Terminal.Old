using SemanticAnalyzer.Exceptions;
using SemanticAnalyzer.Models;

namespace SemanticAnalyzer.DefaultParsers;

public class DefaultParameterParser : IParser
{
    private Dictionary<char, Action<ParsingContext>> _parsingRules = new();

    private const string ParserStep = "Parameter";

    public DefaultParameterParser()
    {
        _parsingRules.Add('"', context =>
        {
            var input = context.CurrentStep;
            var index = input.IndexOf('"', 1);
            if (index == -1) throw new ParsingException(ParserStep, "Closing tag not found");
            index += 1; // get " char
            var strParam = input[..index];
            var parameter = new ParsedParameter(ParsedParameterTypeEnum.Value, strParam.Trim(';', '"'));
            context.ParsedParameters.Add(parameter);
            context.CurrentStep = context.CurrentStep.Replace(strParam, string.Empty)
                .TrimStart(';')
                .Trim();
        });
        _parsingRules.Add('[', context =>
        {
            var input = context.CurrentStep;
            var index = input.IndexOf(']');
            if (index == -1) throw new ParsingException(ParserStep, "Closing tag not found");
            var strParam = input[..(index + 1)]; // get ] char
            var internalContext = new ParsingContext { CurrentStep = strParam.Trim('[', ']') };
            Parse(ref internalContext);
            var parameter = new ParsedParameter(ParsedParameterTypeEnum.Array, internalContext.ParsedParameters);
            context.ParsedParameters.Add(parameter);
            context.CurrentStep = context.CurrentStep.Replace(strParam, String.Empty)
                .TrimStart(';')
                .Trim();
        });
    }
    
    public void Parse(ref ParsingContext context)
    {
        context.ParsedParameters ??= new();
        var counter = 0;
        while (ParseNextParameter(ref context))
        {
            counter++;
            if (counter >= 1000) 
                throw new ParsingException(ParserStep, "To long parsing. Possible stack overflow error");
        }
    }

    private bool ParseNextParameter(ref ParsingContext context)
    {
        var input = context.CurrentStep;
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (input[0] == '-') return false;
        var firstChar = input[0];
        if (_parsingRules.TryGetValue(firstChar, out var rule))
        {
            rule(context);
            return true;
        }

        var index = input.IndexOf(' ');
        var strParam = index != -1 ? input[..(index)] : input;
        var parameter = new ParsedParameter(ParsedParameterTypeEnum.Value, strParam.Trim(';'));
        context.ParsedParameters.Add(parameter);
        context.CurrentStep = context.CurrentStep.Remove(0, strParam.Length).Trim();
        /*context.CurrentStep = context.CurrentStep.Replace(strParam, string.Empty).Trim();*/
        return true;
    }
}