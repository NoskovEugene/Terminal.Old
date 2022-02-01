using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Routing.Models;

namespace Routing.Parsers;

public class DefaultParameterParser : IParser
{
    public ParsingContext Parse(ParsingContext context)
    {
        context.Parameters = new();
        var builder = new StringBuilder(context.CurrentStepLine);
        while (TryParseNextParameter(builder.ToString().Trim(), out var parameter))
        {
            builder.Replace(parameter, string.Empty);
            context.Parameters.Add(parameter.Trim(';').Trim('"'));
        }

        context.CurrentStepLine = builder.ToString().Trim();
        return context;
    }

    public bool TryParseNextParameter(string input, out string parameter)
    {
        parameter = string.Empty;
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (input[0] == '"')
        {
            //long parameter
            var index = input.IndexOf('"', 1) + 1; // get " char 
            if (input.Length != index)
            {
                index = index != input.Length && input[index] == ';' ? index + 1 : index;
                parameter = input[..index];
                return true;
            }
            else
            {
                parameter = input;
                return true;
            }
        }
        else
        {
            // single word parameter
            parameter = input.Contains(';') ? input[..(input.IndexOf(';') + 1)] : input;
            return true;
        }
    }
    
    
}