using Terminal.SemanticAnalyzer.Models;
using Terminal.SharedModels.Models.Routing.Scanner;

namespace Terminal.Routing;

public interface IRouter
{
    void AppendUtilities(List<Utility> utilities);
    bool RemoveRoute(string utilityName);
    List<Command> FindCommands(ParsingContext context);
    List<Route> GetAllRoutes();
    Command? FindByUtilAndCommand(string utilName, string commandName);
}