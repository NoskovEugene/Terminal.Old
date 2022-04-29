using Terminal.SemanticAnalyzer.Models;

namespace Terminal.Routing.Services.Parameter;

public interface IParameterService
{
    IEnumerable<object> PrepareParsedParameters(ParsingContext context);
    void ChangeParametersToPossibleType(ParsingContext context);
}