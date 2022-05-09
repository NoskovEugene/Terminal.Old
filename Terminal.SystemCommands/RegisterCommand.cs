using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Serilog;
using Terminal.Routing;
using Terminal.Routing.Scanner;
using Terminal.SharedModels.Attributes.UtilityAttributes;
using Terminal.SystemCommands.Attributes;

namespace Terminal.SystemCommands;

[SystemUtility]
public class RegisterCommand
{
    private readonly ILogger _logger;
    private IAssemblyScanner _assemblyScanner;
    private WindsorContainer _container;
    private IRouter _router;
    
    public RegisterCommand(ILogger logger, WindsorContainer container, IAssemblyScanner assemblyScanner, IRouter router)
    {
        _logger = logger;
        _container = container;
        _assemblyScanner = assemblyScanner;
        _router = router;
    }
    
    [Command("register")]
    public void Register(string pathToLib)
    {
        if (!File.Exists(pathToLib))
        {
            _logger.Error("File not found");
            return;
        }

        Assembly assembly;
        try
        {
            assembly = Assembly.LoadFile(pathToLib);
        }
        catch (Exception)
        {
            _logger.Error("Assembly cannot be loaded");
            return;
        }

        var utils = _assemblyScanner.ScanAssembly(assembly);
        _router.AppendUtilities(utils);
        var utilTypes = utils.Select(x => x.UtilityType).Distinct();
        foreach (var utilType in utilTypes)
        {
            _container.Register(Component.For(utilType).ImplementedBy(utilType));
        }
        _logger.Information("Commands registered");
    }
}