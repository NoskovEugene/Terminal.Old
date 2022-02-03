using SemanticAnalyzer.Models;

namespace SemanticAnalyzer;

public interface IParser
{
    void Parse(ref ParsingContext context);
}