using Terminal.SemanticAnalyzer.Models;

namespace Terminal.Routing.Services.Parameter.ParameterAnalyze;


public interface IParameterAnalyzeService
{
    IEnumerable<object> PrepareParsedParameters(ParsingContext context);
    void ChangeParametersToPossibleType(ParsingContext context);
    
    bool CheckType<TFrom, TTo>();
}