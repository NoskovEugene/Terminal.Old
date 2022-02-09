namespace SharedModels.Models.Routing.Scanner;

public class Command
{
    public string Name { get; set; }
    
    public IList<Parameter> Parameters { get; set; }
    
    public List<Flag> Flags { get; set; }
}