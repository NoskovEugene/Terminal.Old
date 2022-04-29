namespace Terminal.SemanticAnalyzer.Models;

public class ParsingContext
{
    public string UnparsedLine { get; set; } = string.Empty;
    
    public string CurrentStep { get; set; }
    
    public string ParsedUtilityName { get; set; }
    
    public string ParsedCommandName { get; set; }
    
    public List<ParsedParameter> ParsedParameters { get; set; }
    
    public List<string> ParsedFlags { get; set; }
}
