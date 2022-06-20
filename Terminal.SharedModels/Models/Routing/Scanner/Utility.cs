using System.Reflection;

namespace Terminal.SharedModels.Models.Routing.Scanner;

public class Utility
{
    public string Name { get; set; }
    
    public Type UtilityType { get; set; }

    public IList<Command> Commands { get; set; }
}