using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SemanticAnalyzer;
using SemanticAnalyzer.DefaultParsers;

namespace Core.Helpers;

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