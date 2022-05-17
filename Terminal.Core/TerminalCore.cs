using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Terminal.SemanticAnalyzer;
using Serilog;
using Terminal.Core.Helpers;
using Terminal.Routing;
using Terminal.Routing.Scanner;
using Terminal.SemanticAnalyzer.Models;
using Terminal.SharedModels.Models.Routing.Scanner;
using Terminal.SharedModels.Services.Logging;
using Terminal.SystemCommands;
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
        RegisterSystemCommands();
        _syntaxAnalyzer = Container.Resolve<ISyntaxAnalyzer>();
        _logger = Container.Resolve<ILogger>();
    }

    private void RegisterDependencies()
    {
        Container.Register(Component.For<WindsorContainer>().Instance(Container));
        Container.RegisterDefaultSyntaxService();
        Container.RegisterUiServices();
        Container.Register(Component.For<ILoggerService>().ImplementedBy<LoggerService>().LifestylePooled());
        Container.Register(Component.For<ILogger>()
            .UsingFactoryMethod(kernel => kernel.Resolve<ILoggerService>().ConsoleLogger));
        Container.Register(Component.For<IRouter>().ImplementedBy<Router>().LifestyleSingleton());
        Container.Register(Component.For<IAssemblyScanner>().ImplementedBy<AssemblyScanner>());
    }

    private void RegisterSystemCommands()
    {
        var systemCommands = new List<Type>
        {
            typeof(RegisterCommand),
            typeof(GetAllCommandsCommand),
            typeof(ShowCommandInfoCommand),
            typeof(TestCommands)
        };
        var scanner = Container.Resolve<IAssemblyScanner>();
        var utils = scanner.ScanTypes(systemCommands.ToArray());
        var router = Container.Resolve<IRouter>();
        router.AppendUtilities(utils);
        foreach (var systemCommand in systemCommands)
        {
            Container.Register(Component.For(systemCommand).ImplementedBy(systemCommand));
        }
    }

    public void StartListen()
    {
        var cancel = true;
        var analyzer = Container.Resolve<ISyntaxAnalyzer>();
        var router = Container.Resolve<IRouter>();
        var logger = Container.Resolve<ILogger>();
        while (cancel)
        {
            Console.Write(">_ ");
            var line = Console.ReadLine();
            if (line == "q")
            {
                cancel = false;
                continue;
            }

            if (string.IsNullOrEmpty(line))
            {
                logger.Error("Input line cannot be empty");
                continue;
            }

            ParsingContext ctx;
            try
            {
                ctx = analyzer.ParseInputLine(line);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                continue;
            }

            var commands = router.FindCommands(ctx);
            Command? command = default;
            if (commands == null)
            {
                logger.Error("Commands not found");
                continue;
            }

            if (commands.Count == 1)
                command = commands[0];
            else
            {
                if (commands.Count <= 0)
                {
                    logger.Error("Commands not found");
                    continue;
                }

                if (commands.Count > 1)
                {
                    command = ChooseCommand(commands);
                }
            }

            object instance;
            try
            {
                instance = Container.Resolve(command!.ClassInfo);
            }
            catch (Exception)
            {
                logger.Error("Cannot resolve command class type from container");
                continue;
            }

            var parameters = ctx.ParsedParameters.Select(x => x.Value).ToList();
            if (ctx.ParsedFlags.Count > 0)
                parameters.Add(ctx.ParsedFlags.ToArray());
            try
            {
                command.MethodInfo.Invoke(instance, parameters.ToArray());
            }
            catch (Exception e)
            {
                logger.Fatal(e.Message);
            }
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