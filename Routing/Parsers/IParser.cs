using Routing.Models;

namespace Routing.Parsers;

public interface IParser
{
    ParsingContext Parse(ParsingContext context);
}