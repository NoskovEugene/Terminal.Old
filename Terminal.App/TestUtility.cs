using Serilog;
using Terminal.SharedModels.Attributes.UtilityAttributes;

namespace Terminal;

[Utility("test")]
public class TestUtility2
{
    private ILogger _logger;
    
    public TestUtility2(ILogger logger)
    {
        _logger = logger;
    }

    [Command("add")]
    public void TestMethod(string param1)
    {
        _logger.Information("add from test utility2");
    }

    [Command("remove")]
    public void TestMethod(string param1, string parameter)
    {
        _logger.Information("remove from test utility2");
    }
}

[Utility("test")]
public class TestUtility
{
    private ILogger _logger;
    
    public TestUtility(ILogger logger)
    {
        _logger = logger;
    }

    [Command("update")]
    public void TestMethod(byte parameter1, int parameter2, [Flag]string[] flags)
    {
        _logger.Information("add");
    }

    [Command("delete")]
    public void TestMethod(int parameter1, string parameter2, [Flag]string[] flags)
    {
        _logger.Information("remove");
    }
}