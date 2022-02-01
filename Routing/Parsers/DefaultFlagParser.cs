using System.Text.RegularExpressions;
using Routing.Models;

namespace Routing.Parsers;

public class DefaultFlagParser : IParser
{
    public ParsingContext Parse(ParsingContext context)
    {
        context.Flags = new();
        while (TryParseNextFlag(context.CurrentStepLine, out var flag))
        {
            context.CurrentStepLine = context.CurrentStepLine.Replace(flag, string.Empty).Trim(' ');
            context.Flags.Add(flag);
        }
        return context;
    }

    private bool TryParseNextFlag(string input, out string flag)
    {
        flag = "";
        var regex = new Regex(@"\-\w{1}");
        var match = regex.Match(input);
        if (match.Success)
        {
            flag = match.Value;
            return true;
        }
        return false;
    }
}