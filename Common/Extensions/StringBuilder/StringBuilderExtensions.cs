using System.Text;
namespace Common.Extensions.StringBuilder;

public static class StringBuilderExtensions
{
    public static void NewLine(this System.Text.StringBuilder builder)
    {
        builder.Append("\r\n");
    }
}