using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using SemanticAnalyzer;
using SemanticAnalyzer.DefaultParsers;

namespace Core;

public class TerminalCore
{
    public IConfiguration Configuration { get; }

    public WindsorContainer Container { get; }

    public ISemanticService SemanticService { get; }
    
    public TerminalCore()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .Build();
        Configuration = configuration;
        Container = new WindsorContainer();
        Container.Register(Component.For<IConfiguration>().Instance(configuration));
        ConfigureSemanticService();
    }

    private void ConfigureSemanticService()
    {
        SemanticService.AddParser<DefaultUtilityParser>();
        SemanticService.AddParser<DefaultParameterParser>();
        SemanticService.AddParser<DefaultFlagParser>();
    }
    
    public void StartListen()
    {
        while (true)
        {
            var line = Console.ReadLine();
            var context = SemanticService.ParseInputLine(line);
        }
    }
}