// See https://aka.ms/new-console-template for more information


using System.Reflection;
using Common.Extensions.List;
using SharedModels.Attributes.UtilityAttributes;

namespace Terminal;
public static class Program
{
    public static void Main(string[] args)
    {
        /*var assembly = Assembly.GetExecutingAssembly();
        var assemblyScanner = new AssemblyScanner();
        var utilities = assemblyScanner.Scan(assembly);
        */
        
        
        /*var router = new Router(utilities);
        router.TryFindRoute(context, out var route);*/
        Console.ReadKey();
    }

}


[Utility("test")]
public class TestUtility
{
    public TestUtility()
    {
        
    }

    [Command("add")]
    public void TestMethod(string parameter1, string parameter2, string[] flags)
    {
        
    }
}