using Terminal.SharedModels.Models.Routing.Scanner;

namespace Terminal.Routing;

public class Route
{
    public string UtilityName { get; set; }
    
    public List<Command> Commands { get; set; }
}