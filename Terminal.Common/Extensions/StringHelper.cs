
using Terminal.Common.Extensions.List;

namespace Terminal.Common.Extensions;

public static class StringHelper
{
    public static string SubstringWithConsiderSimilar(this string input, char start, char end, bool checkValues = false, string valueIfCheckFail = "")
    {
        var shouldWrite = false;
        var considerCount = 0;
        var builder = new System.Text.StringBuilder();
        var startIndex = input.IndexOf(start);
        var startInput = 0;
        var endInput = 0;
        for (var i = startIndex; i < input.Length; i++)
        {
            var x = input[i];
            if (x == start)
            {
                considerCount += 1;
                startInput += 1;
                shouldWrite = true;
            }

            if (shouldWrite)
            {
                builder.Append(x);
            }

            if (x == end && considerCount != 0)
            {
                considerCount -= 1;
                endInput += 1;
            }

            if (x == end && considerCount == 0)
            {
                break;
            }
        }
        if (checkValues && startInput != endInput) return valueIfCheckFail; 
        return builder.ToString();
    } 
}