namespace Routing.Models;

public class ParsingContext
{
    public string UnparsedLine { get; set; }
    
    public string UtilityName { get; set; }
    
    public string CommandName { get; set; }
    
    public List<string> Parameters { get; set; }
    
    public List<string> Flags { get; set; }
    
    public string CurrentStepLine { get; set; }
}