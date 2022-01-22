using System.Text;
using Common.Extensions.List;
using Common.Extensions.StringBuilder;

namespace Routing.Models;

public class Command
{
    public string Name { get; set; }
    
    public IList<Parameter> Parameters { get; set; }
    
    public IList<Flag> Flags { get; set; }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"->{Name}").NewLine();
        builder.Append($"-->Parameters").NewLine();
        Parameters.Foreach(x=> builder.Append(x).NewLine());
        builder.Append($"->Flags").NewLine();
        Flags.Foreach(x=> builder.Append(x).NewLine());
        return builder.ToString();
    }
}