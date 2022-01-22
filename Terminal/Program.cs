// See https://aka.ms/new-console-template for more information

using SharedModels.Attributes.UtilityAttributes;

namespace Terminal;
public static class Program
{
    public static void Main(string[] args)
    {
        Console.ReadKey();
    }
}

[Utility]
public class TestUtility
{
    public TestUtility()
    {
        
    }

    [Command]
    [Parameter]
    [Flag]
    public void TestMethod(string parameter1, string parameter2, string[] flags)
    {
        
    }
}