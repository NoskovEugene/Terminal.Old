using Terminal.Core;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        var core = new TerminalCore();
        core.StartListen();
    }
}