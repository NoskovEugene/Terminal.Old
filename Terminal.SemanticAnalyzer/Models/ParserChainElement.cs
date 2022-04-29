namespace Terminal.SemanticAnalyzer.Models;

public class ParserChainElement
{
    public ParserChainElement(int priority, Type parserType)
    {
        Priority = priority;
        ParserType = parserType;
    }

    public int Priority { get; set; }
    
    public Type ParserType { get; }
    
    public IParser? Instance { get; set; }
}