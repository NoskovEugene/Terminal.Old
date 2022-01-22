namespace Routing.Models;

public class Parameter
{
    public string Name { get; set; }
    
    public Type ParameterType { get; set; }

    public override string ToString()
    {
        return $"--->{Name}: {ParameterType.FullName}";
    }
}