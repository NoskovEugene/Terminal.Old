using System.Reflection;

namespace SharedModels.Models.Routing.Scanner;

public class Utility
{
    public string Name { get; set; }
    
    public IList<Command> Commands {get; set; }
    
    public Assembly AssemblyInfo { get; set; }
    
    public Type ClassInfo { get; set; }
    
    public object ClassObject { get; set; }
}