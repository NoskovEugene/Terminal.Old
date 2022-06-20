using System.Reflection;

namespace Terminal.SharedModels.Models.Routing.Scanner;

public class Command
{
    public string Name { get; set; }
    
    public IList<Parameter> Parameters { get; set; }
    
    public List<Flag> Flags { get; set; }
    
    public MethodInfo MethodInfo { get; set; }
    
    public Type ClassInfo { get; set; }
    
    public Assembly AssemblyInfo { get; set; }
}