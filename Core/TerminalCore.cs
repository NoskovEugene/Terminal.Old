using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Helpers;
using Microsoft.Extensions.Configuration;
using SemanticAnalyzer;
using SemanticAnalyzer.DefaultParsers;

namespace Core;

public class TerminalCore
{
    public IConfiguration Configuration { get; }

    public WindsorContainer Container { get; }

    private ISyntaxAnalyzer _syntaxAnalyzer;
    
    public TerminalCore()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .Build();
        Configuration = configuration;
        Container = new WindsorContainer();
        Container.Register(Component.For<IConfiguration>().Instance(configuration));
        Container.RegisterDefaultSyntaxService();
        _syntaxAnalyzer = Container.Resolve<ISyntaxAnalyzer>();
    }

    public void StartListen()
    {
        while (true)
        {
            var line = Console.ReadLine();
            var context = _syntaxAnalyzer.ParseInputLine(line);
            
        }
    }
}