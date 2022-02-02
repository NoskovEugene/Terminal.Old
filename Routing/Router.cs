using Routing.Models;

namespace Routing;

public class Router
{
    protected IList<Utility> Utilities;

    public Router(IList<Utility> utilities)
    {
        Utilities = utilities;
    }

    public bool TryFindRoute(ParsingContext context, out Route route)
    {
        var utility = Utilities.FirstOrDefault(x => x.Name == context.UtilityName);
        if (utility == null)
        {
            route = default;
            return false;
        }

        route = new()
        {
            RequiredUtility = utility
        };
        var requiredCommand = utility.Commands.FirstOrDefault(x => x.Name == context.CommandName && x.Parameters.Count() == context.Parameters.Count());
        if (requiredCommand == null)
        {
            return false;
        }

        route.RequiredCommand = requiredCommand;
        route.Parameters = context.Parameters;
        route.Flags = context.Flags;
        return true;
    }
}