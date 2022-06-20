using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Terminal.SemanticAnalyzer;
using Terminal.SemanticAnalyzer.DefaultParsers;

namespace Terminal.Core.Helpers;

public static class SyntaxAnalyzerHelper
{
    public static void RegisterDefaultSyntaxService(this WindsorContainer container)
    {
        var syntaxAnalyzer = new SyntaxAnalyzer();
        syntaxAnalyzer.AddParser<DefaultUtilityParser>();
        syntaxAnalyzer.AddParser<DefaultParameterParser>();
        syntaxAnalyzer.AddParser<DefaultFlagParser>();
        container.Register(Component.For<ISyntaxAnalyzer>().Instance(syntaxAnalyzer));
    }
}