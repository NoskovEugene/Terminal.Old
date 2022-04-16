// See https://aka.ms/new-console-template for more information

using Serilog;
using SharedModels.Attributes.UtilityAttributes;
using SharedModels.Models.Routing.Scanner;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.ReadKey();
    }
}

public class Route
{
    public Utility Utility { get; set; }
    
    public Command Commands { get; set; }
}

public class RatingResult<TResult>
{
    public int Rating { get; set; }
    
    public TResult Result { get; set; }
}


[Utility("test")]
public class TestUtility
{
    private ILogger _logger;
    
    public TestUtility(ILogger logger)
    {
        _logger = logger;
    }

    [Command("add")]
    public void TestMethod(byte parameter1, int parameter2, [Flag]string[] flags)
    {
        _logger.Information("add");
    }

    [Command("remove")]
    public void TestMethod(int parameter1, string parameter2, [Flag]string[] flags)
    {
        _logger.Information("remove");
    }
}