namespace Terminal.SemanticAnalyzer.Models;

public class ParsedParameter
{
    public ParsedParameterTypeEnum ParameterTypeEnum { get; }
    
    public object Value { get; set; }

    public Type PossibleParameterType { get; set; }
    
    public ParsedParameter(ParsedParameterTypeEnum parameterTypeEnum, object value)
    {
        ParameterTypeEnum = parameterTypeEnum;
        Value = value;
    }
}