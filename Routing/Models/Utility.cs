using System.Text;
using Common.Extensions.List;
using Common.Extensions.StringBuilder;

namespace Routing.Models;

public class Utility
{
    public string Name { get; set; }
    
    public IList<Command> Commands { get; set; }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"{Name}").NewLine();
        Commands.Foreach(x=> builder.Append(x).NewLine());
        return builder.ToString().Trim();
    }
}