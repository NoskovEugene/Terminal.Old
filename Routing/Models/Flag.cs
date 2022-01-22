namespace Routing.Models;

public class Flag
{
    public string Name { get; set; }

    public override string ToString()
    {
        return $"--->{Name}";
    }
}