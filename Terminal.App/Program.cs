using Terminal.Common.MapService;
using Terminal.Common.MapService.Helpers;

namespace Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        /*var core = new TerminalCore();
        var router = core.Container.Resolve<IRouter>();
        var scanner = core.Container.Resolve<IAssemblyScanner>();
        var analyzer = core.Container.Resolve<ISyntaxAnalyzer>();
        var utils = scanner.ScanAssembly(Assembly.GetExecutingAssembly());
        var context = analyzer.ParseInputLine("remove [123 123,123 123 123,123]");
        router.AppendUtilities(utils);
        var parameterService = new ParameterAnalyzeService();
        parameterService.ChangeParametersToPossibleType(context);
        var lst = parameterService.PrepareParsedParameters(context);
        */
        var map = new Map<Type>();
        map.ConfigurePath(x=> x.Source<byte>().Target<short>());
        map.ConfigurePath(x=> x.Source<short>().Target<int>().Target<float>());
        map.ConfigurePath(x=> x.Source<int>().Target<float>());
        map.TryFindPath<byte>(out var path);
    }
}

public class TestClass
{
    public void Method(byte param)
    {
        Console.WriteLine("123");
    }
}