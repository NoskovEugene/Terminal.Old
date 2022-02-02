using Routing.Models;

namespace Routing;

public class Route
{
    public Utility RequiredUtility { get; set; }
    
    public Command RequiredCommand { get; set; }
    
    public List<string> Parameters { get; set; }
    
    public List<string> Flags { get; set; }
}