using System.Text.RegularExpressions;
using Routing.Models;

namespace Routing.Parsers;

public class DefaultUtilityParser : IParser
{
    public ParsingContext Parse(ParsingContext context)
    {
        var input = context.CurrentStepLine;
        var regex = new Regex(@"\w{1,}\.\w{1,}");
        var match = regex.Match(input);
        if (match.Success)
        {
            var util = match.Value;
            var arr = util.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length != 2)
            {
                throw new Exception("");
            }

            context.UtilityName = arr[0];
            context.CommandName = arr[1];
            context.CurrentStepLine = input.Replace(util, string.Empty).Trim(' ');
        }
        else
        {
            throw new Exception("");
        }
        return context;
    }
}