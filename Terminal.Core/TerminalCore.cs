using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Terminal.SemanticAnalyzer;
using Serilog;
using Terminal.Core.Helpers;
using Terminal.Routing;
using Terminal.Routing.Scanner;
using Terminal.Routing.Services.Parameter;
using Terminal.SharedModels.Models.Routing.Scanner;
using Terminal.SharedModels.Services.Logging;
using UI.RequestService;

namespace Terminal.Core;

public class TerminalCore
{
    public WindsorContainer Container { get; }

    private readonly ISyntaxAnalyzer _syntaxAnalyzer;

    private readonly ILogger _logger;
    
    public TerminalCore()
    {
        Container = new WindsorContainer();
        RegisterDependencies();
        _syntaxAnalyzer = Container.Resolve<ISyntaxAnalyzer>();
        _logger = Container.Resolve<ILogger>();
    }

    private void RegisterDependencies()
    {
        Container.Register(Component.For<WindsorContainer>().Instance(Container));
        Container.RegisterDefaultSyntaxService();
        Container.RegisterUiServices();
        Container.Register(Component.For<ILoggerService>().ImplementedBy<LoggerService>().LifestyleSingleton());
        Container.Register(Component.For<ILogger>()
            .UsingFactoryMethod(kernel => kernel.Resolve<ILoggerService>().ConsoleLogger));
        Container.Register(Component.For<IRouter>().ImplementedBy<Router>().LifestyleSingleton());
        Container.Register(Component.For<IAssemblyScanner>().ImplementedBy<AssemblyScanner>());
        Container.Register(Component.For<IParameterService>().ImplementedBy<ParameterService>().LifestyleSingleton());
    }

    public void StartListen()
    {
        _logger.Debug("Start listen");
        var requestService = Container.Resolve<UserRequestService>();
        var router = Container.Resolve<IRouter>();
        var parameterService = Container.Resolve<IParameterService>();
        while (true)
        {
            var line = requestService.RequestLine("");
            var context = _syntaxAnalyzer.ParseInputLine(line);
            parameterService.ChangeParametersToPossibleType(context);
            var commands = router.FindCommands(context);
            if (commands.Count < 1)
            {
                _logger.Error("Command not found");
                continue;
            }

            var command = ChooseCommand(commands);
            var utilObject = Container.Resolve(command.ClassInfo);
            var parameters = context.ParsedParameters.Select(x => x.Value).ToList();
            parameters.AddRange(context.ParsedFlags);
            command.MethodInfo.Invoke(utilObject, parameters.ToArray());
        }
    }

    private Command ChooseCommand(List<Command> commands)
    {
        if (commands.Count == 1) return commands[0];
        var requestService = Container.Resolve<UserRequestService>();
        return requestService.SelectItem(commands, "Found more than one commands\r\nPlease choose one", 
            (value) => value.Name);
    }
}