using Terminal.SemanticAnalyzer.Models;

namespace Terminal.SemanticAnalyzer;

public interface IParser
{
    void Parse(ref ParsingContext context);
}